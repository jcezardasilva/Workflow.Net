using System.Collections.Generic;
using System.Text.Json;

namespace WorkflowNet.Core.Extensions.Dictionary
{
    public static class DictionaryExtensions
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };
        public static T ToModel<T>(this Dictionary<string, object> dictionary) where T : class
        {
            return ToModel<T>(dictionary, _jsonSerializerOptions);
        }
        public static T ToModel<T>(this Dictionary<string,object> dictionary, JsonSerializerOptions jsonSerializerOptions) where T : class
        {
            var json = JsonSerializer.Serialize(dictionary,jsonSerializerOptions);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
