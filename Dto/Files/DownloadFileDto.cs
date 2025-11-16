using MessagePack;

namespace LaciSynchroni.Common.Dto.Files;

[MessagePackObject(keyAsPropertyName: true)]
public record DownloadFileDto : ITransferFileDto
{
    public bool FileExists { get; set; } = true;
    public string Hash { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public long Size { get; set; } = 0;
    // Lightless
    public string? CDNDownloadUrl { get; set; } = null;
    // PlayerSync
    public string? DirectDownloadUrl { get; set; } = null;
    public bool IsForbidden { get; set; } = false;
    public string ForbiddenBy { get; set; } = string.Empty;
    public long RawSize { get; set; } = 0;
}