using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SurrealCB.Data.Enum
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
        COLDING, //Ralentiza X% por X segundos, pero stackea infinitamente
        SHATTER, //Saca X% de vida total al golpear
        DOOM, //Mata luego de X segundos
        BLAZE, //Fuego X danio cada vez que lo golpean por X segundos
        BACKTRACK, //Retrocede X segundos de speed
        BOUNCE, //Rebota y hace X de danio a otro adversario
        BLEED, //Sangra al enemigo, que recibe X de danio por cada ataque realizado durante X segundos
        DODGE, //Evade el ataque con un X% de chances
        IMMUNE, //Ninguna pasiva le hace efecto
        TOUGH, //Un X% de reducir el danio un X
        ENDURABLE, //Reduce el danio final en X%
        GHOST, //No recibe danio directo al ser atacado, solo con efectos secundarios
        BLOWMARK, //Marca al objetivo por cada golpe, anadiendo X danio al proximo ataque
        BLIND, //Ciega al enemigo dejandole un X% de que missee durante X segundos
        DEVIANT, //El ataque es aleatorio entre el atk base y X
        ABLAZE, //Al golpearte, el enemigo recibe X de danio
        SCORCHED, //Al golpearte, el enemigo se quema, recibiendo X por cada ataque durante Y seg
        OBLIVION, //X% de matar al enemigo,
        BURN, //Un X% de daniar al enemigo con Z de danio extra y luego de Y segundos tambien X% de daniar con Z,
        BERSEKER, //Mitad de vida o menos, incrementa danio y velocidad
        KNOCKOUT, //Mata si la vida del enemigo es X o menor
        LIFESTEAL, //Se cura un X% del danio causado
        REFLECT, //Refleja estados en un X%
        REGURGITATE, //Se cura un X por cada ataque
        BURNOUT, //Quema por X de danio y cuando ataca y es atacado
        WINTER, //Reduce spd enemigo en X%
        SURPRISEATTACK, //Start the battle attacking regardless the speed
        ELECTRIFY, //X% de perder el turno durante Y segundos
        THIEF, //Se esconde durante X segundos al atacar, siendo inseleccionable
        DOUBLE_ATTACK, //X% de atacar dos veces
        FLEE, //X% de escapar al ser atacado
        FLAMMABLE, //Take double damage for fire effects
        MELT, //Deals X extra fire dmg on hit
        TIMESHIFT, //Chupa time actual
        CONFUSSION, //La carta golpea al azar a cualquiera del tablero durante X segundos,
        HELLFIRE, //Cada Y segundos dania a todos con X
        TRANSFUSE, //Al golpear, un X% del danio es transferido a un aliado para curarlo
        DEVIATE, //Desvia los ataques por X segundos
        INUNDATE, //Hace que su speed total sea un X mayor por golpe
        FRENZY, //Incrementa el speed por cada ataque
        SPIKE_ARMOR, //Devuelve el X% de danio al atacante
        INTENSIFY, //Incrementa el danio sufrido por efectos un X
        MARKDOWN, //Luego de X marcas dania por Y en el prox ataque
        STRIKER, //Dania al azar por X al empezar la batalla
        INCINERATE, //Damages for X every Y seconds until die or dispelled
        DISPELL, //Elimina efectos negativos al curar --> tiene que usarse con heal
        ENERGYBOMB, //Al morir, explota daniando en X al enemigo
        VENOM, //Poison, cada X danio cada Y segundos reducido por Z cada vez

        //HACER ESTAS LOGICAS
        RUNEBREAKER, //Deshabilita las runas enemigas

        //NO USADO PERO CON ICONO

        TORNADO, //Golpea X veces aleatoriamente
        DEFRAGMENTER, //Saca X% de vida actual al golpear

        //NO USADO NI ICONO

        DEFLECT, //Un X% de desviar el golpe a un companiero
        GUARDIAN, //Mientras este vivo, tiene un X% de recibir los ataques aliados
        DESTROYER, //Aniade X de danio por cada X de hp del oponente
        REFINEMENT, //Incrementa en X el ataque por cada golpe dado
        INCAPACITATE, //Reduce el ataque del enemigo en X durante X segundos
        CLEAVE, //Dania en un X% a los enemigos del costado del atacado
        DISABLE, //Deja inactiva la carta por X segundos (no puede ser atacada ni atacar)
        VANISH, //Reduce en X el ataque propio por cada golpe hasta llegar a 0
        MISTERY, //Golpea en X a pos2 en Y a pos3 y en Z a pos 4
        NULLIFY, //Mientras este vivo, ningun aliado puede sufrir efectos
        TAUNT, //Al golpear hace q el enemigo solo pueda atacarlo a el durante X segundos
        SHIELD, //Brinda X de armadura (como otra barra de hp que no cuenta def para el danio)
        ASSAULT, //Por cada turno enemigo golpea al azar a uno causando X danio
        FROZEN, //Immobilize the target but reduce damage taken durante X segundos
        POISONUS, //Envenena al enemigo que lo ataca, mismos parametros que poison
        ROUGH, //Reduce el danio del enemigo por cada golpe recibido en X
        PARASITE, //Marca a un enemigo, y al morir genera un parasito (a definir)
        ZOMBIE, //Brinda estado zombie, al curar se saca vida
        REVIVE, //Un X% de revivir a alguien al atacar
        EXPLODE, //Marca al enemigo, y al morir explota y dania a todos en X
        BOMB, //Golpea luego de X segundos inflijendo Y danio
        CHEER, //Incrementa atk aliado en X%
        HASTE, //Incrementa spd aliado en X%
        VITALIZER, //Incrementa hp aliado en X%
        CRITICAL, //X% de changes de golpear un Y% mas
        WRECKER, //Add the enemy def to the atk, and ignore defense
        SHURIKEN, //Inflije X danio real y rebota, reduciendo X danio por golpe hasta 0
        SLOWDOWN, //Reduce hasta morir
        SPEEDSTEAL, //Chupa time total stackea hasta morir
        TICTOC, //Que mate una carta al azar cada X ataques (guardar ataques en variable)
        SECOND_CHANCE, //X% de revivir al morir (solo una vez)
        ANULLATION, //Reduces defense to 0 randomly on battle start
        INFEST, //Infecta al enemigo, daniando a algun aliado por cada golpe en X, durante Y segundos
        ROOT, //Enrieda al enemigo, el cual solo podra atacar a su frente. Dura x segundos
        LANDMINE, //Empieza minando al enemigo del frente, y explota luego de X segundos inflinjiendo Y danio
        UNLEASH, //Luego de recibir X golpes, suelta energia pegandole a todos por Y
        TRANSFER, //Al atacar le traspasa los active effects al enemigo
        SHUFFLE, //Al atacar cambia aleatoriamente de lugar al enemigo
        REAPER, //Al morir mata al enemigo de menos hp (siempre y cuando no quede una sola carta)
        GRIM, //Al morir mata al de mas hp (siempre y cuando no quede una sola carta)
        SLUGGISHER, //Hace mas lento al enemigo por cada ataque (permanente)
        DISMANTLE, //Reduce imm a 0 al atacar
        ENFORCER, //Incrementa la def a X por Y segundos al atacar
        GROWER, //Cada X segundos incrementa la HP en Y (tmb cura)
        SPEEDKILLER, //Pega adicional a los mas rapidos (ver como armar fomula)
        SLOWKILLER, //Pega adicional a los mas lentos (ver como armar formula)
        MAGICANULATE, //Anula la carta magica enemiga
        PLENITY, //Lo contrario a berseker (con full hp)
        TEARDROP, //Cura X cada ataque durante Y segundos

        //TODO: NO PASIVAS PERO PUEDE SER FUEGO Y POISON ALL o BLEED
    }
}
