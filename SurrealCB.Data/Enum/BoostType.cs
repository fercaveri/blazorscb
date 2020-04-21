using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BoostType
    {
        HP = 0,
        HPPERC,
        ATK,
        ATKPERC,
        DEF,
        DEFPERC,
        SPD,
        SPDPERC,
        IMM,
        IMMPERC,
        PASSIVEPERC
    }
}
