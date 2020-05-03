using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate.Linq;
using SurrealCB.Data;
using SurrealCB.Data.Model;
using SurrealCB.Data.Repository;

namespace SurrealCB.Server
{
    public interface IUserService
    {
        Task<List<PlayerCard>> GetUserCards(int id = 0);
        Task<List<PlayerRune>> GetUserRunes();
        Task<int> GetUserGold();
        Task<int> GetUserId(string userName = null);
        Task<ApplicationUser> GetUser(int id);
    }
    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<PlayerCard>> GetUserCards(int id = 0)
        {
            if (id == 0)
            {
                var cards = (await this.repository.Query<ApplicationUser>().FirstOrDefaultAsync()).Cards.ToList();
                return cards;
            }
            else
            {
                var cards = (await this.repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.Id == id)).Cards.ToList();
                return cards;
            }

        }

        public async Task<List<PlayerRune>> GetUserRunes()
        {
            //var runes = (await this.repository.Users.FirstOrDefaultAsync()).Runes.Where(x => !this.repository.PlayerRunes.Select(y => y.RuneId).Contains(x.RuneId)).ToList();
            //return runes;
            return null;
        }

        public async Task<int> GetUserGold()
        {
            return (await this.repository.Query<ApplicationUser>().FirstOrDefaultAsync()).Gold;
        }

        public async Task<int> GetUserId(string userName = null)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return (await this.repository.Query<ApplicationUser>().FirstOrDefaultAsync()).Id;
            }
            //Guid userId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(JwtClaimTypes.Subject).Value);
            return (await this.repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.UserName == userName)).Id;
        }

        public async Task<ApplicationUser> GetUser(int id)
        {
            return await this.repository.Query<ApplicationUser>().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
