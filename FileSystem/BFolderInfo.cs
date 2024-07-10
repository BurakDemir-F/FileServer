using System.Text;

namespace WebAppFirst.Models;

public class BFolderInfo
{
    public string Path { get; set; }
    public string Name { get; set; }
    public List<BFileInfo> Files { get; set; }
    public List<BInnerFolder> InnerDirectories { get; set; }

    private BInnerFolder _upperDirectory;

    public BInnerFolder UpperDirectory
    {
        get => _upperDirectory;
        set
        {
            _upperDirectory = value;
            HasUpperDirectory = value != null;
        }
    }

    public bool HasUpperDirectory { get; private set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append($"Current Directory Path: {Path} - ");
        sb.AppendLine();
        sb.AppendLine("Files");
        foreach (var fileInfo in Files)
        {
            sb.AppendLine($"File:{fileInfo.Name} - ");
        }
        
        foreach (var directoryPath in InnerDirectories)
        {
            sb.AppendLine($"Directory: {directoryPath.Name} - ");
        }

        sb.AppendLine();
        return sb.ToString();
    }
}

public class BFileInfo
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Type { get; set; }
}

public class BInnerFolder
{
    public string Path
    {
        get; set;
    }
    
    public string Name { get; set; }
}