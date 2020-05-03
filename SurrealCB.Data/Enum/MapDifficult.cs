using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Enum
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
