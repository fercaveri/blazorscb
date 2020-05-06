using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum AtkType
    {
        NORMAL = 0,
        RANDOM,
        LOWEST_HP,
        ALL,
        HEAL,
        MAGIC
    }
}
