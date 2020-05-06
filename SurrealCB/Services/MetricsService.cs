using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate.Linq;
using SurrealCB.Data;
using SurrealCB.Data.Enum;
using SurrealCB.Data.Model;
using SurrealCB.Data.Repository;

namespace SurrealCB.Server
{
    public interface IMetricService
    {
        Task MakeLevelMetric(int level);
        Task MakeLevelMetricBalanced(int level);
        Task MakeLevelMetricRarity(int level, Rarity rarity);
        Task DoMetricBattle(List<BattleCard> cards);
    }
    public class MetricService : IMetricService
    {
        private readonly IRepository repository;
        private readonly IBattleService battleService;
        private readonly IUserService userService;

        public MetricService(IRepository repository, IBattleService battleService, IUserService userService)
        {
            this.repository = repository;
            this.battleService = battleService;
            this.userService = userService;
        }

        public async Task DoMetricBattle(List<BattleCard> cards)
        {
            var battleStatus = new BattleStatus { Status = BattleEnd.CONTINUE };
            var rounds = 0;
            var nextPosition = -1;
            do
            {
                if (rounds > 150)
                {
                    battleStatus.Status = BattleEnd.DRAW;
                }
                else
                {
                    rounds++;
                    battleStatus = await this.battleService.NextTurn(cards);
                    nextPosition = battleStatus.NextPosition;
                    var card = cards.FirstOrDefault(x => x.Position == nextPosition);
                    var who = -1;
                    BattleCard target = null;
                    var dontStuck = 0;
                    if (card.PlayerCard.Card.AtkType != AtkType.ALL && card.Hp > 0)
                    {
                        var enemiesHasHp = true;
                        do
                        {
                            dontStuck++;
                            var playerCardcount = nextPosition < 4 ? cards.Where(x => x.Position < 4).Count() : cards.Where(x => x.Position > 3).Count();
                            var rand = new Random();
                            who = rand.Next(1, playerCardcount + 1) - 1;
                            if (nextPosition < 4 && card.PlayerCard.Card.AtkType != AtkType.HEAL)
                            {
                                who += 4;
                                enemiesHasHp = cards.Where(x => x.Position > 3).Any(x => x.Hp != 0);
                            }
                            else if (card.PlayerCard.Card.AtkType == AtkType.HEAL && nextPosition > 3)
                            {
                                who += 4;
                            }
                            else
                            {
                                enemiesHasHp = cards.Where(x => x.Position < 4).Any(x => x.Hp != 0);
                            }
                            who = this.ShouldConvertWho(nextPosition, cards);
                            target = cards.FirstOrDefault(x => x.Position == who);
                        }
                        while (target.Hp == 0 && enemiesHasHp && dontStuck < 26 && battleStatus.Status == BattleEnd.CONTINUE);
                        // && !(target.GetPassives().Any(x => x.Passive == Passive.GHOST) && card.GetPassives().Any(x => this.battleService.GetActionType(x.Passive) != HealthChange.DAMAGE) &&
                        //    nextPosition < 4 ? cards.Where(x => x.Position > 3 && x.Position != nextPosition && x.Hp != 0).Any() : cards.Where(x => x.Position < 4 && x.Position != nextPosition && x.Hp != 0).Any()
                        //));
                        await this.battleService.PerformAttack(card, cards.FirstOrDefault(x => x.Position == who), cards);
                        battleStatus.Status = await this.battleService.CheckWinOrLose(cards, nextPosition);
                        if (dontStuck >= 25)
                        {
                            battleStatus.Status = BattleEnd.DRAW;
                        }
                        dontStuck = 0;
                    }
                    else
                    {
                        await this.battleService.AttackAll(card, cards);
                        battleStatus.Status = await this.battleService.CheckWinOrLose(cards, nextPosition);
                    }
                }
            }
            while (battleStatus.Status == BattleEnd.CONTINUE);
            if (battleStatus.Status != BattleEnd.DRAW)
            {
                foreach (var card in cards)
                {
                    await this.repository.GetSession().MergeAsync<CardMetric>(new CardMetric
                    {
                        Card = card.PlayerCard.Card,
                        Level = card.PlayerCard.GetLevel(),
                        Died = card.Hp == 0,
                        Win = (card.Position < 4 && battleStatus.Status == BattleEnd.WIN) || (card.Position > 3 && battleStatus.Status == BattleEnd.LOSE)
                    });
                }
            }
            //await this.repository.SaveCollectionAsync<CardMetric>(cards.Select(card => new CardMetric
            //{
            //    Card = card.PlayerCard.Card,
            //    Level = card.PlayerCard.GetLevel(),
            //    Died = card.Hp == 0,
            //    Win = (card.Position < 4 && battleStatus.Status == BattleEnd.WIN) || (card.Position > 3 && battleStatus.Status == BattleEnd.LOSE)
            //}).ToList());

        }

        public int ShouldConvertWho(int nextPosition, List<BattleCard> cards)
        {
            var source = cards.FirstOrDefault(x => x.Position == nextPosition);
            var timeshift = source.GetPassives().FirstOrDefault(x => x.Passive == Passive.TIMESHIFT);
            if (timeshift != null)
            {
                List<BattleCard> enemies;
                if (nextPosition < 4)
                {
                    enemies = cards.Where(x => x.Position > 3 && x.Hp > 0).ToList();
                }
                else
                {
                    enemies = cards.Where(x => x.Position < 4 && x.Hp > 0).ToList();
                }
                if (!enemies.Any()) return nextPosition;
                var rand = new Random();
                nextPosition = enemies[rand.Next(0, enemies.Count - 1)].Position;
                return nextPosition;
            }
            else if (source.PlayerCard.Card.AtkType == AtkType.HEAL)
            {
                List<BattleCard> allies;
                if (nextPosition < 4)
                {
                    allies = cards.Where(x => x.Position < 4 && x.Hp > 0).ToList();
                }
                else
                {
                    allies = cards.Where(x => x.Position > 3 && x.Hp > 0).ToList();
                }
                if (!allies.Any()) return nextPosition;
                nextPosition = allies.FirstOrDefault(x => x.Hp == allies.Min(x => x.Hp)).Position;
                return nextPosition;
            }
            return nextPosition;
        }

        public async Task MakeLevelMetric(int level)
        {
            var levelId = await this.userService.GetUserId($"level{level}");
            var cards = new List<BattleCard>();
            var random = new Random();
            var user1Cards = await this.userService.GetUserCards(levelId);
            for (var i = 0; i < 8; i++)
            {
                var pcard = user1Cards[random.Next(0, user1Cards.Count)];
                cards.Add(new BattleCard(pcard)
                {
                    Position = i
                });
            }
            await this.DoMetricBattle(cards);
        }

        public async Task MakeLevelMetricBalanced(int level)
        {
            var levelId = await this.userService.GetUserId($"level{level}");
            var cards = new List<BattleCard>();
            var random = new Random();
            var userCards = await this.userService.GetUserCards(levelId);
            var normalCards = userCards.Where(x => x.Card.Rarity == Rarity.COMMON).ToList();
            var rareCards = userCards.Where(x => x.Card.Rarity == Rarity.RARE).ToList();
            var specialCards = userCards.Where(x => x.Card.Rarity == Rarity.SPECIAL).ToList();
            var legendaryCards = userCards.Where(x => x.Card.Rarity == Rarity.LEGENDARY).ToList();
            cards.Add(new BattleCard(normalCards[random.Next(0, normalCards.Count())]) { Position = 0});
            cards.Add(new BattleCard(normalCards[random.Next(0, normalCards.Count())]) { Position = 4 });
            cards.Add(new BattleCard(rareCards[random.Next(0, rareCards.Count())]) { Position = 1 });
            cards.Add(new BattleCard(rareCards[random.Next(0, rareCards.Count())]) { Position = 5 });
            cards.Add(new BattleCard(specialCards[random.Next(0, specialCards.Count())]) { Position = 2 });
            cards.Add(new BattleCard(specialCards[random.Next(0, specialCards.Count())]) { Position = 6 });
            cards.Add(new BattleCard(legendaryCards[random.Next(0, legendaryCards.Count())]) { Position = 3 });
            cards.Add(new BattleCard(legendaryCards[random.Next(0, legendaryCards.Count())]) { Position = 7 });
            await this.DoMetricBattle(cards);
        }

        public async Task MakeLevelMetricRarity(int level, Rarity rarity)
        {
            var levelId = await this.userService.GetUserId($"level{level}");
            var cards = new List<BattleCard>();
            var random = new Random();
            var userCards = (await this.userService.GetUserCards(levelId)).Where(x => x.Card.Rarity == rarity).ToList();
            for (var i = 0; i < 8; i++)
            {
                var pcard = userCards[random.Next(0, userCards.Count())];
                cards.Add(new BattleCard(pcard)
                {
                    Position = i
                });
            }
            await this.DoMetricBattle(cards);
        }
    }
}
