using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Geocodificador.Http
{
    public static class ClienteHttp
    {
        private static HttpClient Cliente { get; set; } = new HttpClient();

        public static async Task<T> GetAsync<T>(
            string uri, 
            Dictionary<string, string> headers = null, 
            Dictionary<string, string> queryString = null
            )
        {
            Cliente.BaseAddress = new Uri(uri);

            headers.Keys
                .ToList()
                .ForEach(key => Cliente.DefaultRequestHeaders.Add(key, headers[key]));

            HttpResponseMessage respuesta = await Cliente.GetAsync(QueryHelpers.AddQueryString(uri, queryString));

            var resultadoJson = respuesta.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<T>(resultadoJson);
        }
    }
}
