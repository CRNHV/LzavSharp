using System;
using System.Runtime.InteropServices;

namespace LzavSharp;
internal static class LzavWrapper
{
    private const string DllName = "Lzav.dll"; // Replace with your actual DLL name

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int lzav_compress_bound(int srcl);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int lzav_compress_bound_hi(int srcl);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int lzav_compress_default(IntPtr src, IntPtr dst, int srcl, int dstl);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int lzav_compress_hi(IntPtr src, IntPtr dst, int srcl, int dstl);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int lzav_decompress(IntPtr src, IntPtr dst, int srcl, int dstl);

    /// <summary>
    /// Compresses the input data using the default LZAV compression.
    /// </summary>
    public static byte[] CompressDefault(byte[] input)
    {
        if (input == null || input.Length == 0)
            throw new ArgumentException("Input buffer cannot be null or empty.");

        int maxLen = lzav_compress_bound(input.Length);
        byte[] compressed = new byte[maxLen];

        IntPtr srcPtr = Marshal.AllocHGlobal(input.Length);
        IntPtr dstPtr = Marshal.AllocHGlobal(maxLen);

        try
        {
            Marshal.Copy(input, 0, srcPtr, input.Length);
            int compressedLen = lzav_compress_default(srcPtr, dstPtr, input.Length, maxLen);

            if (compressedLen == 0 && input.Length != 0)
                throw new Exception("Compression failed.");

            Array.Resize(ref compressed, compressedLen);
            Marshal.Copy(dstPtr, compressed, 0, compressedLen);
            return compressed;
        }
        finally
        {
            Marshal.FreeHGlobal(srcPtr);
            Marshal.FreeHGlobal(dstPtr);
        }
    }

    /// <summary>
    /// Compresses the input data using the high-ratio LZAV compression.
    /// </summary>
    public static byte[] CompressHi(byte[] input)
    {
        if (input == null || input.Length == 0)
            throw new ArgumentException("Input buffer cannot be null or empty.");

        int maxLen = lzav_compress_bound_hi(input.Length);
        byte[] compressed = new byte[maxLen];

        IntPtr srcPtr = Marshal.AllocHGlobal(input.Length);
        IntPtr dstPtr = Marshal.AllocHGlobal(maxLen);

        try
        {
            Marshal.Copy(input, 0, srcPtr, input.Length);
            int compressedLen = lzav_compress_hi(srcPtr, dstPtr, input.Length, maxLen);

            if (compressedLen == 0 && input.Length != 0)
                throw new Exception("High-ratio compression failed.");

            Array.Resize(ref compressed, compressedLen);
            Marshal.Copy(dstPtr, compressed, 0, compressedLen);
            return compressed;
        }
        finally
        {
            Marshal.FreeHGlobal(srcPtr);
            Marshal.FreeHGlobal(dstPtr);
        }
    }

    /// <summary>
    /// Decompresses the input data.
    /// </summary>
    public static byte[] Decompress(byte[] compressedData, int originalSize)
    {
        if (compressedData == null || compressedData.Length == 0)
            throw new ArgumentException("Compressed data cannot be null or empty.");
        if (originalSize <= 0)
            throw new ArgumentException("Original size must be greater than zero.");

        byte[] decompressed = new byte[originalSize];

        IntPtr srcPtr = Marshal.AllocHGlobal(compressedData.Length);
        IntPtr dstPtr = Marshal.AllocHGlobal(originalSize);

        try
        {
            Marshal.Copy(compressedData, 0, srcPtr, compressedData.Length);
            int decompressedLen = lzav_decompress(srcPtr, dstPtr, compressedData.Length, originalSize);

            if (decompressedLen < 0)
                throw new Exception("Decompression failed.");

            Marshal.Copy(dstPtr, decompressed, 0, originalSize);
            return decompressed;
        }
        finally
        {
            Marshal.FreeHGlobal(srcPtr);
            Marshal.FreeHGlobal(dstPtr);
        }
    }
}
