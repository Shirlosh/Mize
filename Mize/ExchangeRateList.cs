using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Mize
{
    public class ExchangeRateList
    {
        public string Base { get; set; }
        public Dictionary<string,string> Rates { get; set; }
        public int Timestamp {  get; set; }
        public string Disclaimer{  get; set; }
        public string License{  get; set; }

        public override string ToString()
        {
            return $"Timestamp:{Timestamp} \nBase:{Base} \nRates: {
                Rates.Select(a => $"{a.Key}: {a.Value}{Environment.NewLine}").Aggregate((a,b)=>a+b)
            }";
        }
    }
}
