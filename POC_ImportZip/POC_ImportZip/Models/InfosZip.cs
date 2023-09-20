namespace POC_ImportZip.Models
{
    public record InfosZip(
        byte[]? bytesImage,
        string FileName,
        int Width,
        int Height,
        long Size
        );
}
