using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrokodyliWeb.Utils
{
    public class ExtensionStorage
    {
        public interface IExtensionOwner<T>
        {
            public T CreateExtensionInstance();
        }


        private readonly Dictionary<Type, object> _data = new();

        public T Get<T>(IExtensionOwner<T> owner)
        {
            if (!_data.TryGetValue(typeof(T), out var ret))
            {
                ret = owner.CreateExtensionInstance();
                _data[typeof(T)] = ret;
            }
            return (T)ret;
        }
    }
}
