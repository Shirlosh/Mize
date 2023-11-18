using Newtonsoft.Json;
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
            var r = new ChainableExchange();
            Console.WriteLine(await r.GetValue());
            Console.WriteLine(await r.GetValue());

        }
    }
}
