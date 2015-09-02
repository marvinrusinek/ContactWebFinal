﻿using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace koListEditor
{
    public class FromJsonAttribute : CustomModelBinderAttribute
    {
        private readonly static JavaScriptSerializer serializer = new JavaScriptSerializer();

        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }

        private class JsonModelBinder : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                var stringified = controllerContext.HttpContext.Request[bindingContext.ModelName];
                if (string.IsNullOrEmpty(stringified))
                    return null;
                var model = serializer.Deserialize(stringified, bindingContext.ModelType);
                return model;
            }
        }
    }
}