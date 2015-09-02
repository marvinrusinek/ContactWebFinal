using System;
using System.Collections.Generic;


namespace ekCommonLibs.IOC
{
    public class ConfigIoC
    {
        static readonly IDictionary<string, object> types = new Dictionary<string, object>();

        public static ConfigIoC Instance
        {
            get
            {
                return new ConfigIoC();
            }
        }

        public void Register(string key, object instance)
        {
            types[key] = instance;
        }

        public T Resolve<T>()
        {
            var key = typeof(T).FullName;
            if (!types.ContainsKey(key))
                throw new Exception(String.Format("{0} can not be found in dictionary", key));
            return (T)types[key];
        }

        public void Init()
        {
            var appSettings = System.Web.Configuration.WebConfigurationManager.AppSettings;
            var webKeys = appSettings.Keys;
            var filteredKeys = new List<string>();
            foreach(var setting in webKeys)
            {
                if(setting.ToString().StartsWith("IoC:"))
                    filteredKeys.Add(setting.ToString());
            }

            foreach (var filteredKey in filteredKeys)
            {
                var _interfacePart = filteredKey.Substring("IoC:".Length);
                var _implementationPart = appSettings[filteredKey];
                
                Type interfaceType =
                    System.Reflection.Assembly.Load(_interfacePart.Split(',')[0]).GetType(_interfacePart.Split(',')[1]);

                object instance = System.Reflection.Assembly.Load(_implementationPart.Split(',')[0]).CreateInstance(_implementationPart.Split(',')[1]);


                this.Register(interfaceType.FullName, instance);
            }
        }
    }
}
