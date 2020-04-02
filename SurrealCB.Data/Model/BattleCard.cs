using System;
using System.Collections.Generic;

namespace SurrealCB.Data.Model
{
    public class BattleCard : IEntity
    {
        public int Position { get; set; }
        public int Hp { get; set; }
        public double Spd { get; set; }
        public List<CardPassive> ActiveEffects { get; set; }
        public PlayerCard PlayerCard { get; set; }

        public BattleCard(PlayerCard pcard)
        {
            this.PlayerCard = pcard;
            this.Hp = pcard.GetHp();
            this.Spd = pcard.GetSpd();
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

        public double GetImmunity()
        {
            var immunity = this.PlayerCard.GetImm();
            //TODO: Active effs
            return immunity;
        }

        public double GetSpeed()
        {
            var speed = this.PlayerCard.GetSpd();
            //TODO: Active effs
            return speed;
        }
    }
}
