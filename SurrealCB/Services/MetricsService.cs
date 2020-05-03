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

            var battleStatus = new BattleStatus { Status = BattleEnd.CONTINUE };
            var rounds = 0;
            var nextPosition = -1;
            do
            {
                if (rounds == 150)
                {
                    return;
                }
                rounds++;
                battleStatus = await this.battleService.NextTurn(cards);
                nextPosition = battleStatus.NextPosition;
                var card = cards.FirstOrDefault(x => x.Position == nextPosition);
                var who = -1;
                if (card.PlayerCard.Card.AtkType != AtkType.ALL)
                {
                    var enemiesHasHp = true;
                    do
                    {
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
                    }
                    while (enemiesHasHp && cards.FirstOrDefault(x => x.Position == who).Hp == 0 && battleStatus.Status == BattleEnd.CONTINUE);
                    await this.battleService.PerformAttack(card, cards.FirstOrDefault(x => x.Position == who), cards);
                    battleStatus.Status = await this.battleService.CheckWinOrLose(cards, nextPosition);
                }
                else
                {
                    await this.battleService.AttackAll(card, cards);
                    battleStatus.Status = await this.battleService.CheckWinOrLose(cards, nextPosition);
                }
            }
            while (battleStatus.Status == BattleEnd.CONTINUE);
            await this.repository.SaveCollectionAsync<CardMetric>(cards.Select(card => new CardMetric
            {
                Card = card.PlayerCard.Card,
                Level = card.PlayerCard.GetLevel(),
                Died = card.Hp == 0,
                Win = card.Position < 4 && battleStatus.Status == BattleEnd.WIN
            }).ToList());

        }
    }
}
