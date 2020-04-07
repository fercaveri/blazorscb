using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MapDifficult
    {
        EASY = 0,
        NORMAL,
        HARD,
        INSANE,
        HELL
    }
}
