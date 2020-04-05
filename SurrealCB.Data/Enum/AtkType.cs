using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AtkType
    {
        NORMAL = 0,
        RANDOM,
        ALL,
        HEAL,
        MAGIC
    }
}
