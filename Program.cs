using System.Net;
using System.Net.Sockets;
using Cocona;
using WebAppFirst.Services;

namespace WebAppFirst;

public class Program
{
    private static string _port = "8000";

    public static void Main(string[] args)
    {
        var builder = CoconaApp.CreateBuilder();
        var app = builder.Build();

        app.AddCommand((string path,string port) =>
        {
            RunNetCoreApp(args,path,port);
        });

        app.Run();
    }

    private static void RunNetCoreApp(string[] args,string path, string port)
    {
        _port = port;
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.

        var url = GetServeUrl();
        builder.WebHost.UseUrls(url);
        builder.WebHost.UseSetting("StartPath", path);
        builder.WebHost.UseSetting("Port", port);
        
        builder.Services.AddRazorPages();
        builder.Services.AddSingleton<FileSystemService>();
        builder.Services.AddServerSideBlazor();

        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.MapControllerRoute(name: "default", pattern: "{controller=Index}/{action}/{id?}");

        app.UseAuthorization();
        app.MapRazorPages();
        app.MapControllers();
        app.MapBlazorHub();

        app.Run();
    }
    
    private static string GetServeUrl()
    {
        return $"http://{GetLocalIpAddress()}:{_port}";
    }

    private static string GetLocalIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }

        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}