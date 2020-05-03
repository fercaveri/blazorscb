using System;
using System.Collections.Generic;
using System.Linq;

namespace SurrealCB.Data.Model
{
    public class BattleCard : IEntity
    {
        public int Position { get; set; }
        public int Hp { get; set; }
        public double Time { get; set; }
        public virtual List<ActiveEffect> ActiveEffects { get; set; } = new List<ActiveEffect>();
        public virtual PlayerCard PlayerCard { get; set; }

        public BattleCard(PlayerCard pcard)
        {
            this.PlayerCard = pcard;
            this.Hp = pcard.GetHp();
            this.Time = pcard.GetSpd();
            if (this.GetPassives().Any(x => x.Passive == Passive.SURPRISEATTACK))
            {
                this.Time = 0;
            }
            var hellfire = this.GetPassives().FirstOrDefault(x => x.Passive == Passive.HELLFIRE);
            if (hellfire != null)
            {
                this.ActiveEffects.Add(new ActiveEffect
                {
                    FromPosition = this.Position,
                    Passive = Passive.HELLFIRE,
                    Param1 = hellfire.Param1,
                    Param2 = hellfire.Param2,
                    Param3 = hellfire.Param3
                });
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
                }

            }
            return speed;
        }
    }
}
