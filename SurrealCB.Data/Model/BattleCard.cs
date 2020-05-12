using System;
using System.Collections.Generic;
using System.Linq;
using SurrealCB.Data.Enum;

namespace SurrealCB.Data.Model
{
    public class BattleCard : IEntity
    {
        public int Position { get; set; }
        public int Hp { get; set; }
        public double Time { get; set; }
        public int Shield { get; set; }
        public virtual ICollection<ActiveEffect> ActiveEffects { get; set; } = new List<ActiveEffect>();
        public virtual PlayerCard PlayerCard { get; set; }

        public BattleCard(PlayerCard pcard)
        {
            var starterPassives = new[] { Passive.HELLFIRE, Passive.STRIKER };
            this.PlayerCard = pcard;
            this.Hp = pcard.GetHp();
            this.Time = pcard.GetSpd();
            if (this.GetPassives().Any(x => x.Passive == Passive.SURPRISEATTACK))
            {
                this.Time = 0;
            }

            foreach (var passive in starterPassives)
            {
                var selected = this.GetPassives().FirstOrDefault(x => x.Passive == passive);
                if (selected != null)
                {
                    this.ActiveEffects.Add(new ActiveEffect
                    {
                        FromPosition = this.Position,
                        Passive = selected.Passive,
                        Param1 = selected.Param1,
                        Param2 = selected.Param2,
                        Param3 = selected.Param3
                    });
                }
            }
        }

        public BattleCard()
        {

        }

        public List<CardPassive> GetPassives()
        {
            return this.PlayerCard.GetPassives();
        }

        public int GetHp()
        {
            var hp = this.PlayerCard.GetHp();
            //TODO: Active effs
            return hp;
        }

        public int GetAtk()
        {
            var attack = this.PlayerCard.GetAtk();
            foreach (var activeEff in this.ActiveEffects)
            {
                switch (activeEff.Passive)
                {
                    case Passive.BERSEKER:
                        attack += (int)activeEff.Param1;
                        break;
                }

            }
            return attack;
        }

        public int GetDef()
        {
            var deffense = this.PlayerCard.GetDef();
            //TODO: Active effs
            return deffense;
        }

        public int GetImm()
        {
            var immunity = this.PlayerCard.GetImm();
            //TODO: Active effs
            return immunity;
        }

        public double GetSpd()
        {
            var speed = this.PlayerCard.GetSpd();
            foreach (var activeEff in this.ActiveEffects)
            {
                switch (activeEff.Passive)
                {
                    case Passive.BERSEKER:
                        speed -= (int)activeEff.Param2;
                        break;
                    case Passive.INUNDATE:
                        speed += (int)activeEff.Param1;
                        break;
                    case Passive.FRENZY:
                        speed -= (int)activeEff.Param1;
                        break;
                }

            }
            return Math.Max(0.3, speed);
        }
    }
}
