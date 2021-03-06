﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurrealCB.Data.Enum;
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
            Passive.SHATTER => Tuple.Create($"Reduce {p1}% of enemy total hp on every hit.", "shatter"),
            Passive.DEFRAGMENTER => Tuple.Create($"Reduce {p1}% of enemy current hp on every hit.", "defragmenter"),
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
            Passive.BURN => Tuple.Create($"Has a {p1}% chance to burn the enemy, inflicting {p2} extra dmg and repeating the chance after {p3} seconds.", "burn"),
            Passive.BERSEKER => Tuple.Create($"When the hp goes below 50%, increase the attack by {p1} and speed by {p2}.", "berseker"),
            Passive.KNOCKOUT => Tuple.Create($"Kill the enemy if his hp is below or equal to {p1}.", "knockout"),
            Passive.RUNEBREAKER => Tuple.Create($"Disable all enemy runes until die.", "runebreaker"),
            Passive.LIFESTEAL => Tuple.Create($"Regain {p1}% of the dealt damage as hp.", "lifesteal"),
            Passive.REFLECT => Tuple.Create($"Has a {p1}% chances to reflect the passive effect.", "reflect"),
            Passive.REGURGITATE => Tuple.Create($"Heals {p1} on every hit.", "regurgitate"),
            Passive.BURNOUT => Tuple.Create($"Inflicts {p1} dmg every time the card is hit or attacks, during for {p2} seconds.", "burnout"),
            Passive.WINTER => Tuple.Create($"Reduce the enemies speed by {p1}% until this card dies.", "winter"),
            Passive.SURPRISEATTACK => Tuple.Create($"Start the battle attacking regardless the speed.", "surpriseattack"),
            Passive.ELECTRIFY => Tuple.Create($"Has a {p1}% to lose the turn during, during {p2} seconds.", "electrify"),
            Passive.THIEF => Tuple.Create($"Hides for {p1} seconds on attack, making it unselectable.", "thief"),
            Passive.DOUBLE_ATTACK => Tuple.Create($"{p1}% to perform a double attack.", "double_attack"),
            Passive.ENDURABLE => Tuple.Create($"Reduces the final dmg (after applying defenses) by {p1}%.", "endurable"),
            Passive.FLEE => Tuple.Create($"{p1}% chances to flee after being attacked.", "flee"),
            Passive.FLAMMABLE => Tuple.Create($"Takes double damage for fire effects.", "flammable"),
            Passive.SCORCHED => Tuple.Create($"Scorch every enemy that attacks you, receiving {p1} dmg every time the enemy is hit, for {p2} seconds.", "scorched"),
            Passive.MELT => Tuple.Create($"Deal {p1} extra dmg on hit.", "melt"),
            Passive.TIMESHIFT => Tuple.Create($"Absorbs {p1}% of the elapsed time, reducing time for next turn and increasing that amout to the enemy.", "timeshift"),
            Passive.CONFUSSION => Tuple.Create($"Confuses the enemy, making it attacking his teammates, during {p1} seconds.", "confussion"),
            Passive.TRANSFUSE => Tuple.Create($"Heals {p1}% of the dealt damage to an ally.", "transfuse"),
            Passive.HELLFIRE => Tuple.Create($"Deals {p1} damage to all enemies every {p2} seconds.", "hellfire"),
            Passive.DEVIATE => Tuple.Create($"Deviate the enemy attacks for {p1} seconds.", "deviate"),
            Passive.INUNDATE => Tuple.Create($"Slow {p1} the total speed of the target. Stacks on every hit.", "inundate"),
            Passive.FRENZY => Tuple.Create($"Increases the speed by {p1} on every hit.", "frenzy"),
            Passive.SPIKE_ARMOR => Tuple.Create($"Deal {p1}% dmg of the damage taken.", "spike_armor"),
            Passive.INTENSIFY => Tuple.Create($"Increase damage taken by effects by {p1}%.", "intensify"),
            Passive.STRIKER => Tuple.Create($"Deal randomly {p1} damage at start of the battle.", "striker"),
            Passive.COLDING => Tuple.Create($"Slows the speed {p1}% for {p2} seconds. Stack & reset timer on every hit.", "colding"),
            Passive.INCINERATE => Tuple.Create($"Deal {p1} dmg every {p2} seconds until die or dispelled.", "incinerate"),
            Passive.DISPELL => Tuple.Create($"Remove target effects on heal.", "dispell"),
            Passive.ENERGYBOMB => Tuple.Create($"When die, deal {p1} damage to the killer card.", "energybomb"),
            Passive.MARKDOWN => Tuple.Create($"After hitting {p1} times, deals {p2} extra damage.", "markdown"),
            Passive.VENOM => Tuple.Create($"Poison the enemy, dealting initial {p1} damage, every {p2} seconds, reducing by {p3} every time.", "venom"),
            Passive.SHIELD => Tuple.Create($"Shield the target ally for {p1}.", "shield"),
            _ => Tuple.Create("", ""),
        };
    }
}
