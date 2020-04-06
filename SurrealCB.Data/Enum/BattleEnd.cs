using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BattleEnd
    {
        CONTINUE = 0,
        WIN,
        LOSE,
        DRAW
    }
}
