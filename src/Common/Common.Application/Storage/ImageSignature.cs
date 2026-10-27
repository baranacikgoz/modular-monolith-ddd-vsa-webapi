namespace Common.Application.Storage;

/// <summary>Recognises the three accepted image formats from the first bytes of the file, never from the client's
/// extension or content type: the bucket is public, so a mislabelled file (an SVG or HTML page) must not get in.</summary>
public static class ImageSignature
{
    private static readonly byte[] Jpeg = [0xFF, 0xD8, 0xFF];
    private static readonly byte[] Png = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A];
    private static readonly byte[] Riff = [0x52, 0x49, 0x46, 0x46];
    private static readonly byte[] Webp = [0x57, 0x45, 0x42, 0x50];

    public static bool TryDetect(ReadOnlySpan<byte> content, out string contentType, out string extension)
    {
        if (content.StartsWith(Jpeg))
        {
            contentType = "image/jpeg";
            extension = ".jpg";
            return true;
        }

        if (content.StartsWith(Png))
        {
            contentType = "image/png";
            extension = ".png";
            return true;
        }

        // RIFF container: bytes 0-3 "RIFF", 4-7 the chunk size, 8-11 "WEBP".
        if (content.Length >= 12 && content[..4].SequenceEqual(Riff) && content.Slice(8, 4).SequenceEqual(Webp))
        {
            contentType = "image/webp";
            extension = ".webp";
            return true;
        }

        contentType = string.Empty;
        extension = string.Empty;
        return false;
    }
}
