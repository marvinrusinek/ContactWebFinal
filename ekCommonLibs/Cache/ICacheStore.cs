using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ekCommonLibs.Cache
{
    public interface ICacheStore
    {
        void Set(string key, object value);
        object Get(string key);
    }
}
