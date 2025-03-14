namespace LzavSharp.Tests;

using System;
using System.Text;
using Xunit;

public class LzavSharpTests
{
    private readonly Encoding _encoding = Encoding.UTF8;

    [Fact]
    public void CompressDefault_ShouldCompressAndDecompressCorrectly()
    {
        string originalText = "This is a test string for compression!";
        byte[] originalData = _encoding.GetBytes(originalText);

        byte[] compressedData = LzavSharp.CompressDefault(originalData);
        Assert.NotNull(compressedData);
        Assert.NotEmpty(compressedData);       
        byte[] decompressedData = LzavSharp.Decompress(compressedData, originalData.Length);
        Assert.Equal(originalData, decompressedData);
    }

    [Fact]
    public void CompressHi_ShouldCompressAndDecompressCorrectly()
    {
        string originalText = "Another string to test high-ratio compression.";
        byte[] originalData = _encoding.GetBytes(originalText);

        byte[] compressedData = LzavSharp.CompressHi(originalData);
        Assert.NotNull(compressedData);
        Assert.NotEmpty(compressedData);
        
        byte[] decompressedData = LzavSharp.Decompress(compressedData, originalData.Length);
        Assert.Equal(originalData, decompressedData);
    }

    [Fact]
    public void CompressDefault_WithStringInput_ShouldWork()
    {
        string originalText = "Testing string compression.";
        byte[] compressedData = LzavSharp.CompressDefault(originalText, _encoding);

        Assert.NotNull(compressedData);
        Assert.NotEmpty(compressedData);
    }

    [Fact]
    public void CompressHi_WithStringInput_ShouldWork()
    {
        string originalText = "High-ratio compression test.";
        byte[] compressedData = LzavSharp.CompressHi(originalText, _encoding);

        Assert.NotNull(compressedData);
        Assert.NotEmpty(compressedData);
    }

    [Fact]
    public void Decompress_ShouldFailForIncorrectOriginalSize()
    {
        string originalText = "Short test";
        byte[] originalData = _encoding.GetBytes(originalText);
        byte[] compressedData = LzavSharp.CompressDefault(originalData);

        Assert.Throws<Exception>(() => LzavSharp.Decompress(compressedData, originalData.Length + 10));
    }

    [Fact]
    public void CompressDefault_EmptyInput_ShouldThrowException()
    {
        byte[] emptyInput = new byte[0];
        Assert.Throws<ArgumentException>(() => LzavSharp.CompressDefault(emptyInput));
    }

    [Fact]
    public void CompressHi_EmptyInput_ShouldThrowException()
    {
        byte[] emptyInput = new byte[0];
        Assert.Throws<ArgumentException>(() => LzavSharp.CompressHi(emptyInput));
    }

    [Fact]
    public void Decompress_InvalidData_ShouldFail()
    {
        byte[] invalidData = new byte[] { 1, 2, 3, 4, 5 };
        Assert.Throws<Exception>(() => LzavSharp.Decompress(invalidData, 100));
    }
}
