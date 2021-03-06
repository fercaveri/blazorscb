using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate.Linq;
using SurrealCB.Data;
using SurrealCB.Data.Model;
using SurrealCB.Data.Repository;
using SurrealCB.Server.Misc;

namespace SurrealCB.Server
{
    public interface ICardService
    {
        Task<List<Card>> GetAll();
        Task ActivateLevelBoost(PlayerCard card, LevelBoost lb);
    }
    public class CardService : ICardService
    {
        private readonly IRepository repository;
        private readonly IUserService userService;

        public CardService(IRepository repository, IUserService userService)
        {
            this.repository = repository;
            this.userService = userService;
        }

        public async Task<List<Card>> GetAll()
        {
            var cards = await this.repository.Query<Card>().ToListAsync();
            return cards;
        }

        public async Task ActivateLevelBoost(PlayerCard card, LevelBoost lb)
        {
            var userGold = await this.userService.GetUserGold();
            //TODO: BORRAR!!!
            userGold = 10000000;
            if (lb.Cost > userGold)
            {
                throw new ApiException("Gold insufficent", 400);
            }
            card.ActiveLvlBoosts.Add(new ActiveLevelBoost { LevelBoost = lb/*, PlayerCardId = card.Id*/});
            await this.repository.SaveAsync(card);
        }
    }
}
