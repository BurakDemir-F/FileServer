using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppFirst.Services;

namespace WebAppFirst.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public readonly FileSystemService FileSystemService;
    public IndexModel(ILogger<IndexModel> logger, FileSystemService fileSystemService)
    {
        _logger = logger;
        FileSystemService = fileSystemService;
    }

    public void OnGet()
    {
    }
}