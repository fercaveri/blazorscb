using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum GameType
    {
        NORMAL = 0,
        ENDURACE,
        DECK,
    }
}
