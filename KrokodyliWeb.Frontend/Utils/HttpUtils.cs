using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Text;

namespace KrokodyliWeb.Frontend.Utils
{
    public static class HttpUtils
    {
        public static async Task<string> GetStringNoCacheAsync(this HttpClient self, string uri)
        {

            var req = new HttpRequestMessage(HttpMethod.Get, uri);
            req.SetBrowserRequestCache(BrowserRequestCache.NoCache);
            using var stream = await (await self.SendAsync(req)).Content.ReadAsStreamAsync();
            return await new StreamReader(stream).ReadToEndAsync();
        }
    }
}
