using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KrokodyliWeb.Utils
{
    public static class WebUtils
    {
        public static async Task<T> GetJsonAsync<T>(this HttpClient self, string uri)
        {
            return await JsonSerializer.DeserializeAsync<T>(await self.GetStreamAsync(uri));
        }
    }
}
