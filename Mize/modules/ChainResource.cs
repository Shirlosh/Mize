using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mize
{
    public abstract class ChainResource<T>
    {
        protected List<Storage<T>> storages = new List<Storage<T>>();

        public Task<T> GetValue()
        {
            List<Action<T>> setters = new List<Action<T>>();
            T data = default;
            bool assigned = false;
            foreach (var s in storages)
            {
                if (!s.IsExpired())
                {
                    data = s.GetData().Result;
                    setters.ForEach(s => s(data));
                    assigned = true;
                    break;
                }
                else
                {
                    setters.Add(s.SetData);

                }
            }

            if (!assigned)
            {
                throw new Exception("Chain implementation doesnt contain valid read storage");
            }

            return Task.FromResult(data);
        }
    }
}
