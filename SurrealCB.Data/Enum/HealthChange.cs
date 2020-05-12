using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HealthChange
    {
        DAMAGE = 0,
        HEAL,
        SHIELD,
        DEATH,
        POISON,
        BLAZE,
        BLEED,
        REFLECT,
        FLEE,
    }
}
