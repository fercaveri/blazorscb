using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class BattleCard : IEntity
    {
        public int Position { get; set; }
        public int Hp { get; set; }
        public double Time { get; set; }
        public virtual List<ActiveEffect> ActiveEffects { get; set; }
        public virtual PlayerCard PlayerCard { get; set; }

        public BattleCard(PlayerCard pcard)
        {
            this.PlayerCard = pcard;
            this.Hp = pcard.GetHp();
            this.Time = pcard.GetSpd();
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
            //TODO: Active effs
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
            //TODO: Active effs
            return speed;
        }
    }
}
