using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Passive
    {
        POISON = 0, //Veneno X danio cada X segundos
        PIERCING, //Atraviesa X defensa (resta el numero a la def)
        IGNORE_DEF, //Danio verdadero (no tiene en cuenta defensa)
        STUN, //Deja aturdido X segundos
        FREEZE, //Ralentiza X% por X segundos
        HP_SCATTER, //Saca X% de vida total al golpear
        DOOM, //Mata luego de X segundos
        BLAZE, //Fuego X danio cada vez que toca golpear
    }
}
