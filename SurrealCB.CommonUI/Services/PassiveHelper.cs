using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurrealCB.Data.Model;

namespace SurrealCB.CommonUI.Services
{
    public class PassiveHelper
    {
        public static Tuple<string, string> GetPassiveTuple(Passive p, double p1 = 0, double p2 = 0, double p3 = 0) =>
        p switch
        {
            Passive.BLAZE => Tuple.Create($"Inflicts {p1} dmg every time the card is hit, during for {p2} seconds.", "blaze"),
            Passive.BLEED => Tuple.Create($"Inflicts {p1} dmg every card turn for {p2} seconds.", "bleed"),
            Passive.DOOM => Tuple.Create($"Kill the card attacked in {p1} seconds.", "doom"),
            Passive.FREEZE => Tuple.Create($"Slows the speed {p1}% for {p2} seconds.", "freeze"),
            Passive.SHATTER => Tuple.Create($"Reduce {p1} of enemy total hp on every hit.", "shatter"),
            Passive.DEFRAGMENTER => Tuple.Create($"Reduce {p1} of enemy current hp on every hit.", "defragmenter"),
            Passive.IGNORE_DEF => Tuple.Create($"Ignore entire enemy defense.", "ignore_defense"),
            Passive.PIERCING => Tuple.Create($"Ignore {p1} of defense on attack.", "piercing"),
            Passive.POISON => Tuple.Create($"Inflicts {p1} dmg every {p2} seconds for {p3} seconds.", "poison"),
            Passive.STUN => Tuple.Create($"Stun the target for {p1} seconds.", "stun"),
            Passive.BACKTRACK => Tuple.Create($"Backtrack the target speed for {p1} seconds.", "backtrack"),
            Passive.DODGE => Tuple.Create($"{p1}% chance of evasion.", "dodge"),
            Passive.BOUNCE => Tuple.Create($"Bounces the damage, inflicting {p1} damage to a second target.", "bounce"),
            Passive.TORNADO => Tuple.Create($"{p1}% chance of evasion.", "tornado"),
            Passive.IMMUNE => Tuple.Create($"Passives doesn't have effect on this card.", "immune"),
            Passive.TOUGH => Tuple.Create($"Has a {p1}% of reduce damage by {p2}.", "tough"),
            Passive.GHOST => Tuple.Create($"Only can be damaged via effects.", "ghost"),
            Passive.BLOWMARK => Tuple.Create($"Mark enemies on hit, dealing {p1} extra dmg for every mark.", "blowmark"),
            Passive.BLIND => Tuple.Create($"Blind the target, missing the {p1}% of the attacks for {p2} seconds.", "blind"),
            Passive.DEVIANT => Tuple.Create($"The attack deals random damage between the base dmg and {p1}.", "deviant"),
            Passive.ABLAZE => Tuple.Create($"Deals {p1} damage to enemies that hits you.", "ablaze"),
            Passive.OBLIVION => Tuple.Create($"Has a {p1}% chance to insta-kill the enemy.", "oblivion"),
            Passive.BURN => Tuple.Create($"Has a {p1}% chance to burn the enemy, inflicting {p2} extra dmg and repeating it after {p3} seconds.", "burn"),
            Passive.BERSEKER => Tuple.Create($"When the hp goes below 50%, increase the attack by {p1} and speed by {p2}.", "berseker"),
            Passive.KNOCKOUT => Tuple.Create($"Kill the enemy if his hp is below {p1}.", "knockout"),
            Passive.LIFESTEAL => Tuple.Create($"Regain {p1}% of the dealt damage as hp.", "lifesteal"),
            Passive.REFLECT => Tuple.Create($"Has a {p1}% chances to reflect the passive effect.", "reflect"),
            _ => Tuple.Create("", ""),
        };
    }
}
