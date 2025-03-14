using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LzavSharp;

public static class LzavSharp
{
    public static byte[] CompressDefault(byte[] input)
    {
        return LzavWrapper.CompressDefault(input);
    }

    public static byte[] CompressDefault(string input, Encoding encoding)
    {
        return LzavWrapper.CompressDefault(encoding.GetBytes(input));
    }

    public static byte[] CompressHi(byte[] input)
    {
        return LzavWrapper.CompressHi(input);
    }

    public static byte[] CompressHi(string input, Encoding encoding)
    {
        return LzavWrapper.CompressHi(encoding.GetBytes(input));
    }

    public static byte[] Decompress(byte[] compressedData, int originalSize)
    {
        return LzavWrapper.Decompress(compressedData, originalSize);
    }
}
