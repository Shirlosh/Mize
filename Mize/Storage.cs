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

        public Task<T>? GetData()
        {
            return ExpirationInterval == null || ExpirationDate > DateTime.Now ? getData() : null;
        }

        public void SetData(T data)
        {
            if (Permission == Permissions.ReadWrite)
            {
                setData(data);
                ExpirationDate = ExpirationInterval != null ?  DateTime.Now.Add((TimeSpan)ExpirationInterval) : ExpirationDate;
            }
        }

    }

    public enum Permissions
    {
        ReadOnly,
        ReadWrite,
    }

}
