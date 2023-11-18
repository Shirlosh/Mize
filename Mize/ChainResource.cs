using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mize
{
    public class ChainResource
    {
        IChainable<string> Chainable;

        public ChainResource(IChainable<string> chainable) 
        {
            Chainable = chainable;

        }
        public Task<string> GetValue()
        {
            List<Action<string>> setters = new List<Action<string>>();
            string? data = null;
            foreach (var s in Chainable.GetIterableStorage())
            {
                data = s.GetData()?.Result;
                if (data == null) 
                {
                    setters.Add(s.SetData);
                }
                else 
                {
                    foreach (var setter in setters)
                    {
                        setter(data);
                    }
                    break;
                }
            }
            return Task.FromResult(data);
        }
    }
}
