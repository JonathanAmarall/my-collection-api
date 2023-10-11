using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace MyCollection.Core.Helpers
{

    public static class NewtonsoftJsonHelper
    {
        public static JsonSerializerSettings IsoDatetimeJsonSettings => new JsonSerializerSettings
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateParseHandling = DateParseHandling.None,
            DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
            Converters = new List<JsonConverter>
        {
            new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffzzzz"
            }
        }
        };

        public static JsonSerializerSettings GetJsonSerializerSettings(NamingStrategy? namingStrategy = null, string dateFormatString = null)
        {
            if (string.IsNullOrWhiteSpace(dateFormatString))
            {
                dateFormatString = "yyyy-MM-ddTHH:mm:ss";
            }

            return new JsonSerializerSettings
            {
                DateFormatString = dateFormatString,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = namingStrategy
                },
                Converters = { (JsonConverter)new StringEnumConverter() }
            };
        }

        public static JsonSerializerSettings GetCustomJsonSerializerSettings(NamingStrategy namingStrategy, IHttpContextAccessor httpContextAccessor, string dateFormatString = null)
        {
            if (string.IsNullOrWhiteSpace(dateFormatString))
            {
                dateFormatString = "yyyy-MM-ddTHH:mm:ss";
            }

            return new JsonSerializerSettings
            {
                DateFormatString = dateFormatString,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CustomControllerJsonFormatterNamingStrategy(namingStrategy, httpContextAccessor)
                },
                Converters = { (JsonConverter)new StringEnumConverter() }
            };
        }

        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static string ToJson(this object value, JsonSerializerSettings jsonSerializerSettings)
        {
            return JsonConvert.SerializeObject(value, jsonSerializerSettings);
        }

        public static StringContent ToJsonContent(this object value, string mediaType = "application/json")
        {
            return new StringContent(value.ToJson(), Encoding.UTF8, mediaType);
        }

        public static T ToObject<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static T ToObject<T>(this string value, JsonSerializerSettings jsonSerializerSettings)
        {
            return JsonConvert.DeserializeObject<T>(value, jsonSerializerSettings);
        }

        public static T ToObject<T>(this string value, Type type)
        {
            return (T)JsonConvert.DeserializeObject(value, type);
        }

        public static async Task<T> ToObject<T>(this HttpContent value)
        {
            return (await value.ReadAsStringAsync()).ToObject<T>();
        }

        public static bool TryParseToObject<T>(this HttpContent value, out T serializedObject)
        {
            try
            {
                string result = value.ReadAsStringAsync().Result;
                serializedObject = result.ToObject<T>();
                return true;
            }
            catch (Exception)
            {
                serializedObject = default(T);
                return false;
            }
        }

        public static async Task<T> ToObject<T>(this HttpContent value, JsonSerializerSettings serializerSettings)
        {
            return (await value.ReadAsStringAsync()).ToObject<T>(serializerSettings);
        }

        public static bool TryParseToObject<T>(this HttpContent value, JsonSerializerSettings serializerSettings, out T serializedObject)
        {
            try
            {
                string result = value.ReadAsStringAsync().Result;
                serializedObject = result.ToObject<T>(serializerSettings);
                return true;
            }
            catch (Exception)
            {
                serializedObject = default(T);
                return false;
            }
        }

        public static object ToSnakeCaseJson(this object value)
        {
            return JsonConvert.DeserializeObject(value.ToJson(new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            }));
        }
    }
}
