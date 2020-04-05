using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CancelStatus
    {
        NONE = 0,
        EVADE,
        MISS,
        STOP,
    }
}
