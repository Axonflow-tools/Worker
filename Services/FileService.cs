namespace Worker.Services;

public static class FileService
{
    private static string? _currentFile { get; set; }

    public static string? CurrentFile
    {
        get => _currentFile;
        set => _currentFile = value;
    }
}