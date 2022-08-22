using Blazored.Modal;
using KrokodyliWeb.Data;
using KrokodyliWeb.Frontend;
using KrokodyliWeb.Frontend.Components;
using KrokodyliWeb.Frontend.Utils;
using KrokodyliWeb.Utils;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using System.Text.Json;
using System.Xml.Serialization;

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
            var navbarTreeConfig = await LoadXml<NavbarTreeConfig>(http, config.NavbarTreeConfigURI);
            
            builder.Services.AddSingleton<WebpageConfig>(sp => config);
            builder.Services.AddSingleton<WebpageData>(sp =>data );
            builder.Services.AddSingleton<NavbarTreeConfig>(sp =>navbarTreeConfig);
            builder.Services.AddSingleton<ExtensionStorage>();
            builder.Services.AddScoped<MarkdownFragment.TranslationsCache>();


            builder.Services.AddBlazoredModal();
            builder.Services.AddRadzen();

            await builder.Build().RunAsync();
        }

        private static async Task<T> LoadJson<T>(HttpClient http, string uri)
        {
            var jsonString = await http.GetStringNoCacheAsync(uri);
            Console.WriteLine($"JSON({uri}):\n{jsonString}--------------------------------------------------------\n\n");
            return JsonSerializer.Deserialize<T>(jsonString)!;
        }
        private static async Task<T> LoadXml<T>(HttpClient http, string uri)
        {
            var xmlString = await http.GetStringNoCacheAsync(uri);
            using var str = new StringReader(xmlString);
            Console.WriteLine($"XML({uri}):\n{xmlString}--------------------------------------------------------\n\n");
            return (T) new XmlSerializer(typeof(T)).Deserialize(str)!;
        }
    }
}