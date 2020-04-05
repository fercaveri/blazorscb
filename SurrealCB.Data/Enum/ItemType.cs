using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Model
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
