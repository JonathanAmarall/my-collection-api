using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Serialization;

namespace MyCollection.Core.Helpers
{
    public class CustomControllerJsonFormatterNamingStrategy : NamingStrategy
    {
        private readonly NamingStrategy _defaultNamingStrategy;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomControllerJsonFormatterNamingStrategy(NamingStrategy defaultNamingStrategy,
            IHttpContextAccessor httpContextAccessor)
        {
            _defaultNamingStrategy = defaultNamingStrategy;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override string ResolvePropertyName(string name)
        {
            return GetControllerCustomSerializationAttributeOption()
                .GetPropertyName(name, false);
        }

        private NamingStrategy GetControllerCustomSerializationAttributeOption()
        {
            return (NamingStrategy)_httpContextAccessor.HttpContext.Items[BaseJsonFormatterAttribute.GetKey] ??
                   _defaultNamingStrategy;
        }
        public abstract class BaseJsonFormatterAttribute : Attribute, IFilterMetadata
        {
            public abstract NamingStrategy GetNamingStrategy();
            public static string GetKey => "CustomJsonFormatter";
        }
    }
}
