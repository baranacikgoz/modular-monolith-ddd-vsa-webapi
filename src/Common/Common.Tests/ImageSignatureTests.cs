using Common.Application.Storage;
using Xunit;

namespace Common.Tests;

#pragma warning disable CA1707 // Remove the underscores from member name
public class ImageSignatureTests
{
    [Theory]
    [InlineData(new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 }, "image/jpeg", ".jpg")]
    [InlineData(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, 0x00 }, "image/png", ".png")]
    [InlineData(new byte[] { 0x52, 0x49, 0x46, 0x46, 0x10, 0x00, 0x00, 0x00, 0x57, 0x45, 0x42, 0x50 }, "image/webp", ".webp")]
    public void TryDetect_KnownSignature_ReturnsContentTypeAndExtension(byte[] content, string contentType, string extension)
    {
        var detected = ImageSignature.TryDetect(content, out var actualContentType, out var actualExtension);

        Assert.True(detected);
        Assert.Equal(contentType, actualContentType);
        Assert.Equal(extension, actualExtension);
    }

    [Fact]
    public void TryDetect_HtmlPageLabelledAsImage_IsRejected()
    {
        var content = "<html><body>not an image</body></html>"u8.ToArray();

        var detected = ImageSignature.TryDetect(content, out var contentType, out var extension);

        Assert.False(detected);
        Assert.Equal(string.Empty, contentType);
        Assert.Equal(string.Empty, extension);
    }

    [Fact]
    public void TryDetect_RiffContainerThatIsNotWebp_IsRejected()
    {
        // A WAV file shares the RIFF header; bytes 8-11 are "WAVE", not "WEBP".
        byte[] content = [0x52, 0x49, 0x46, 0x46, 0x10, 0x00, 0x00, 0x00, 0x57, 0x41, 0x56, 0x45];

        Assert.False(ImageSignature.TryDetect(content, out _, out _));
    }

    [Fact]
    public void TryDetect_EmptyContent_IsRejected()
    {
        Assert.False(ImageSignature.TryDetect(ReadOnlySpan<byte>.Empty, out _, out _));
    }
}
