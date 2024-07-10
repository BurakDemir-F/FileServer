using WebAppFirst.Models;

namespace WebAppFirst.FileSystem;

public class FileSystemProvider
{
    public bool TryGetFolderInfo(string path, out BFolderInfo bFolderInfo)
    {
        if (!Path.Exists(path))
        {
            bFolderInfo = null;
            return false;
        }

        var directoryInfo = new DirectoryInfo(path);
        bFolderInfo = new BFolderInfo();
        bFolderInfo.Name = directoryInfo.Name;
        bFolderInfo.Path = path;
        bFolderInfo.InnerDirectories = new List<BInnerFolder>();
        bFolderInfo.Files = new List<BFileInfo>();
        var upperDirectoryExists = directoryInfo.Parent != null;
        if (upperDirectoryExists)
        {
            bFolderInfo.UpperDirectory = new BInnerFolder()
                { Name = directoryInfo.Parent.Name, Path = directoryInfo.Parent.FullName };
        }
        
        foreach (var directory in directoryInfo.EnumerateDirectories())
        {
            var innerDirectory = new BInnerFolder() { Path = directory.FullName, Name = directory.Name};
            bFolderInfo.InnerDirectories.Add(innerDirectory);
        }

        foreach (var file in directoryInfo.EnumerateFiles())
            bFolderInfo.Files.Add(new BFileInfo(){Name = file.Name,Path = file.FullName, Type = file.Extension});

        return true;
    }
}