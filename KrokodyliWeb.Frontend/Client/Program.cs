using Blazored.Modal;
using KrokodyliWeb.Data;
using KrokodyliWeb.Frontend;
using KrokodyliWeb.Frontend.Pages;
using KrokodyliWeb.Frontend.Utils;
using KrokodyliWeb.Utils;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using System.Text.Json;

namespace KrokodyliWeb.Frontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            HttpClient httpProvider(IServiceProvider? _=null) => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
            builder.Services.AddScoped(httpProvider);

            var http = httpProvider();
            var config = await LoadJson<WebpageConfig>(http, "config.json");
            var data = await LoadJson<WebpageData>(http, config.DataFileURI);
            
            builder.Services.AddSingleton<WebpageConfig>(sp => config);
            builder.Services.AddSingleton<WebpageData>(sp =>data );
            builder.Services.AddSingleton<ExtensionStorage>();
            builder.Services.AddScoped<MarkdownPage.TranslationsCache>();


            builder.Services.AddBlazoredModal();
            builder.Services.AddRadzen();

            await builder.Build().RunAsync();
        }

        private static async Task<T> LoadJson<T>(HttpClient http, string uri)
        {
            var jsonString = await http.GetStringAsync(uri);
            Console.WriteLine($"JSON({uri}):\n{jsonString}--------------------------------------------------------\n\n");
            return JsonSerializer.Deserialize<T>(jsonString)!;
        }
    }
}