using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GameType
    {
        NORMAL = 0,
        ENDURACE,
        DECK,
    }
}
