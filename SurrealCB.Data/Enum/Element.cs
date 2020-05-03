using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Element
    {
        WATER = 0,
        FIRE,
        NATURE,
        LIGHT,
        DARK,
        EARTH,
        WIND,
        NONE
    }
}
