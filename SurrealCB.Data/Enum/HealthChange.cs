using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum HealthChange
    {
        DAMAGE = 0,
        HEAL,
        DEATH,
        POISON,
        BLAZE,
        BLEED,
    }
}
