using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurrealCB.Data;
using SurrealCB.Data.Model;

namespace SurrealCB.Server
{
    public interface IUserService
    {
        Task<List<PlayerCard>> GetUserCards();
        Task<List<PlayerRune>> GetUserRunes();
        Task<int> GetUserGold();
        Task<Guid> GetUserId();
        Task<ApplicationUser> GetUser(Guid id);
    }
    public class UserService : IUserService
    {
        private readonly SCBDbContext repository;

        public UserService(SCBDbContext repository)
        {
            this.repository = repository;
        }

        public async Task<List<PlayerCard>> GetUserCards()
        {
            var cards = (await this.repository.Users.FirstOrDefaultAsync()).Cards.ToList();
            return cards;
        }

        public async Task<List<PlayerRune>> GetUserRunes()
        {
            var runes = (await this.repository.Users.FirstOrDefaultAsync()).Runes.Where(x => !this.repository.PlayerRunes.Select(y => y.RuneId).Contains(x.RuneId)).ToList();
            return runes;
        }

        public async Task<int> GetUserGold()
        {
            return (await this.repository.Users.FirstOrDefaultAsync()).Gold;
        }

        public async Task<Guid> GetUserId()
        {
            //Guid userId = new Guid(_httpContextAccessor.HttpContext.User.FindFirst(JwtClaimTypes.Subject).Value);
            return (await this.repository.Users.FirstOrDefaultAsync()).Id;
        }

        public async Task<ApplicationUser> GetUser(Guid id)
        {
            return await this.repository.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
