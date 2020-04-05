using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Model
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
