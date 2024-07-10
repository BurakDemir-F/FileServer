using System.Text;
using System.Web;
using WebAppFirst.FileSystem;
using WebAppFirst.Models;

namespace WebAppFirst.Services;

public class FileSystemService
{
    private FileSystemProvider _fileSystemProvider;
    private BFolderInfo _startFolderInfo;
    public FileSystemService(IWebHostEnvironment environment)
    {
        _fileSystemProvider = new FileSystemProvider();
        _startFolderInfo = GetStartFolderInfo();
    }

    public BFolderInfo GetStartFolderInfo()
    {
        var directory = Directory.GetCurrentDirectory();
        var hasFileInfo = _fileSystemProvider.TryGetFolderInfo(directory, out var fileInfo);
        fileInfo.UpperDirectory = null;
        return hasFileInfo ? fileInfo : null;
    }
    public BFolderInfo GetFolderInfo(string path)
    {
        var hasFolderInfo = _fileSystemProvider.TryGetFolderInfo(path, out var folderInfo);
        if (!hasFolderInfo)
            return null;
        
        if (IsStartFolder(folderInfo))
            folderInfo.UpperDirectory = null;
        return folderInfo;
    }

    private bool IsStartFolder(BFolderInfo folderInfo)
    {
        if (_startFolderInfo == null)
        {
            return false;
        }

        return _startFolderInfo.Name == folderInfo.Name;
    }

    public string EncodeUrl(BFileInfo fileInfo)
    {
        return HttpUtility.UrlEncode(fileInfo.Path);
    }

    public string ToDefaultEncoding(string utf8Path)
    {
        var bytes = Encoding.UTF8.GetBytes(utf8Path);
        return Encoding.Default.GetString(bytes);
    }
}