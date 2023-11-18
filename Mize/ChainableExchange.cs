using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace Mize
{
    public class ChainableExchange : ChainResource<ExchangeRateList>
    {
        private ExchangeRateList? memory;
        private readonly string apiUrl = "https://openexchangerates.org/api/latest.json?app_id=d1d4939133b94995a7c05f4b3621ec23";
        private readonly string fsPath = "path.json";

        public ChainableExchange()
        {
            storages.Add(new Storage<ExchangeRateList>(Permissions.ReadWrite, GetMemory, SetMemory, new TimeSpan(1, 0, 0)));
            storages.Add(new Storage<ExchangeRateList>(Permissions.ReadWrite, GetFS, SetFS, new TimeSpan(4, 0, 0)));
            storages.Add(new Storage<ExchangeRateList>(Permissions.ReadWrite, GetWebService, null));
        }

        private Task<ExchangeRateList> GetMemory()
        {
            if (Utils.Debug) Console.WriteLine("memory get");
            return Task.FromResult(memory);
        }
        private void SetMemory(ExchangeRateList data)
        {
            if (Utils.Debug) Console.WriteLine("memory set");
            memory = data;
        }
        private async Task<ExchangeRateList> GetFS()
        {
            if (Utils.Debug) Console.WriteLine("fs get");
            return JsonConvert.DeserializeObject<ExchangeRateList>(Utils.ReadFromFile(fsPath));
        }
        private void SetFS(ExchangeRateList data)
        {
            if (Utils.Debug) Console.WriteLine("fs set");
            Utils.WriteToFile(fsPath, data.ToString());
        }
        private async Task<ExchangeRateList> GetWebService()
        {
            if (Utils.Debug) Console.WriteLine("api get");
            var body = await Utils.ApiRequest(apiUrl);
            return JsonConvert.DeserializeObject<ExchangeRateList>(body);
        }

    }
}
