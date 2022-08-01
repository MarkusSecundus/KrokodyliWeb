using KrokodyliWeb.Data;
using KrokodyliWeb.Frontend;
using KrokodyliWeb.Utils;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<WebpageConfig>(sp => new());
            builder.Services.AddSingleton<WebpageData>(sp =>
            {
                return new() { Contacts = new() { new WebpageData.ContactsInfo { Email = "krokodyli@seznam.cz", PersonName = "Martina Barvíøová", PhoneNumber = "123456789" } } };
                /*var cfg = sp.GetRequiredService<WebpageConfig>();
                var httpClient = sp.GetRequiredService<HttpClient>();
                return httpClient.GetJsonAsync<WebpageData>(cfg.DataFileURI).Result;*/
            });

            await builder.Build().RunAsync();
        }
    }
}