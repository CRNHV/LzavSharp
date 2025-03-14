# LzavSharp

LzavSharp is a C# wrapper library around the [LZAV](https://github.com/avaneev/lzav) algorithm. 

The API is accesible through the `LzavSharp` class. Documentation on the LZAV API can be found in the [LZAV repository](https://github.com/avaneev/lzav) or in the [LZAV Directory](/LZAV/)

```csharp
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
```
