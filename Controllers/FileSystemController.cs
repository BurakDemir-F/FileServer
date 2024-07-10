using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using WebAppFirst.Services;

namespace WebAppFirst.Controllers;

[ApiController]
[Route("[controller]")]
public class FileSystemController : ControllerBase
{
    private FileSystemService _fileSystemService;    
    public FileSystemController(FileSystemService service)
    {
        _fileSystemService = service;
    }
    
    [HttpGet]
    [Route("GetFile")]
    public IActionResult GetFile([FromQuery]string path)
    {
        path = HttpUtility.UrlDecode(path);
        var fileStream = new FileStream(path, FileMode.Open,FileAccess.Read);
        var fileStreamResult = new FileStreamResult(fileStream, GetContentType(path));
        return fileStreamResult;
    }

    private string GetContentType(string path)
    {
        new FileExtensionContentTypeProvider().TryGetContentType(path, out var contentType);
        return contentType ?? "application/octet-stream";
    }
}