using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mize
{
    public class Storage<T>
    {
        private TimeSpan? ExpirationInterval;
        private Func<Task<T>> getData;
        private Action<T> setData;
        private Permissions Permission;
        private DateTime ExpirationDate;


        public Storage(Permissions permission, Func<Task<T>> getData, Action<T> setData, TimeSpan? expirationInterval = null)
        {
            ExpirationInterval = expirationInterval;
            this.getData = getData;
            this.setData = setData;
            Permission = permission;
            ExpirationDate = DateTime.Now;
        }

        public Task<T> GetData()
        {
            if (IsExpired())
            {
                throw new InvalidOperationException("cannot read from expired storage");
            }
            return getData();
        }

        public void SetData(T data)
        {
            if (Permission == Permissions.ReadOnly)
            {
                throw new InvalidOperationException("cannot write to readonly storage");

            }
            setData(data);
            ExpirationDate = ExpirationInterval != null ? DateTime.Now.Add((TimeSpan)ExpirationInterval) : ExpirationDate;
        }
        public bool IsExpired()
        {
            return ExpirationInterval != null && ExpirationDate < DateTime.Now;
        }
    }



    public enum Permissions
    {
        ReadOnly,
        ReadWrite,
    }

}
