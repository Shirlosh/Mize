using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mize
{
    public static class Utils
    {
        public static bool Debug = true;
        public static async Task<string> ApiRequest(string apiUrl)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(apiUrl),
                Headers = { { "accept", "application/json" }, },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }

        public static void WriteToFile(string path, string data)
        {
            File.WriteAllText(path, data);
        }

        public static string ReadFromFile(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
