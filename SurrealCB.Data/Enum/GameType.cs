using System;
using Newtonsoft.Json;
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
