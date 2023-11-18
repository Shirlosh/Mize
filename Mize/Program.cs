using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mize
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var erl = new ExchangeRateList();
            var r = new ChainResource(erl);
            Console.WriteLine(await r.GetValue());
            Console.WriteLine(await r.GetValue());
            //File.WriteAllText("path.json", "hello");

        }
    }
}
