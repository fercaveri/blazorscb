using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Passive
    {
        NONE = 0,
        POISON, //Veneno X danio cada X segundos
        PIERCING, //Atraviesa X defensa (resta el numero a la def)
        IGNORE_DEF, //Danio verdadero (no tiene en cuenta defensa)
        STUN, //Deja aturdido X segundos
        FREEZE, //Ralentiza X% por X segundos
        HP_SHATTER, //Saca X% de vida total al golpear
        DOOM, //Mata luego de X segundos
        BLAZE, //Fuego X danio cada vez que lo golpean por X segundos
        BACKTRACK, //Retrocede X segundos de speed
        BOUNCE, //Rebota y hace X de danio a otro adversario
        TORNADO, //Golpea X veces aleatoriamente
        HP_DEFRAGMENTER, //Saca X% de vida actual al golpear
        BLEED, //Sangra al enemigo, que recibe X de danio por cada ataque durante X segundos
        DODGE, //Evade el ataque con un X% de chances

        //TODAVIA EN PROCESO

        SPIKE_ARMOR, //Devuelve el X% de danio al atacante
        BERSEKER, //Incrementa en X el ataque por cada golpe dado
        DISPELL, //Elimina efectos negativos al curar --> tiene que usarse con heal
        ATK_SHATTER, //Reduce el ataque del enemigo en X durante X segundos
        
        
    }
}
