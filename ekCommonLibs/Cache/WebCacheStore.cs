using System;
using System.Web.Caching;

namespace ekCommonLibs.Cache
{
    public class WebCacheStore : ICacheStore
    {
        public System.Web.Caching.Cache Cache
        {
            get { return System.Web.HttpContext.Current.Cache; }
        }
        public void Set(string key, object value)
        {
            Cache[key] = value;
        }

        public object Get(string key)
        {
            return Cache[key];
        }
    }
}
