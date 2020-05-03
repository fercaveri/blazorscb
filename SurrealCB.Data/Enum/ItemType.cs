using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ItemType
    {
        MATERIAL = 0,
        CONSUMIBLE,
        CARD_PACK,
        RECIPE
    }
}
