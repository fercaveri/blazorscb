using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MapDifficult
    {
        EASY = 0,
        NORMAL,
        HARD,
        HARDCORE,
        HELL
    }
}
