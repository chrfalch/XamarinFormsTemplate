using System;
using Newtonsoft.Json;

namespace Test.NewSolution.Data.Clients
{
    /// <summary>
    /// Json serializer.
    /// </summary>
    public static class JsonSerializer
    {
        /// <summary>
        /// Deserializes the json string into the object of type TType
        /// </summary>
        public static TType Deserialize<TType>(string json)
        {
            return JsonConvert.DeserializeObject<TType>(json);
        }

        /// <summary>
        /// Serializes object of type TType to a json string
        /// </summary>
        public static string Serialize<TType>(TType obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}

