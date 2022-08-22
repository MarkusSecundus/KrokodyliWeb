using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Text;

namespace KrokodyliWeb.Frontend.Utils
{
    public static class HttpUtils
    {
        public static Task<string> GetStringNoCacheAsync(this HttpClient self, string uri)
            => GetStringNoCacheAsync_impl(self, new HttpRequestMessage(HttpMethod.Get, uri));

        public static Task<string> GetStringNoCacheAsync(this HttpClient self, Uri uri)
            => GetStringNoCacheAsync_impl(self, new HttpRequestMessage(HttpMethod.Get, uri));

        private static async Task<string> GetStringNoCacheAsync_impl(HttpClient self, HttpRequestMessage req)
        {
            req.SetBrowserRequestCache(BrowserRequestCache.NoCache);
            using var stream = await (await self.SendAsync(req)).Content.ReadAsStreamAsync();
            return await new StreamReader(stream).ReadToEndAsync();
        }
    }
}
