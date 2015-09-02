using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ekCommonLibs.Cache
{
    public class NullCacheStore : ICacheStore
    {
        public void Set(string key, object value)
        {

        }

        public object Get(string key)
        {
            return null;
        }
    }
}
