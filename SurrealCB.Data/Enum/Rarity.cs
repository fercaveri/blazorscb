using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Rarity
    {
        COMMON = 0,
        RARE,
        SPECIAL,
        LEGENDARY,
        EPIC,
        ANCIENT,
        BOSS
    }
}
