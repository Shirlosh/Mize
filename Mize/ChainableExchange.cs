using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Mize
{
    public class ChainableExchange : IChainable<string>
    {
        List<Storage<string>> storages = new List<Storage<string>>();
        string? memory;

        public ChainableExchange()
        {
            storages.Add(new Storage<string>(Permissions.ReadWrite, GetMemory, SetMemory, new TimeSpan(1, 0, 0)));
            storages.Add(new Storage<string>(Permissions.ReadWrite, GetFS, SetFS, new TimeSpan(4, 0, 0)));
            storages.Add(new Storage<string>(Permissions.ReadWrite, GetWebService, null));
        }

        private Task<string> GetMemory()
        {
            return Task.FromResult(memory);
        }
        private void SetMemory(string data)
        {
            memory = data;
        }
        private Task<string> GetFS()
        {
            return Task.FromResult(File.ReadAllText("path.json"));
        }
        private void SetFS(string data)
        {
            File.WriteAllText("path.json", data);

        }
        private async Task<string> GetWebService()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://openexchangerates.org/api/latest.json?app_id=d1d4939133b94995a7c05f4b3621ec23"),
                Headers = { { "accept", "application/json" }, },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
        }


        public List<Storage<string>> GetIterableStorage()
        {
            return storages;
        }
    }
}
