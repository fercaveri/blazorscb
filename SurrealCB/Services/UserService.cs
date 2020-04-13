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
        Task<int> GetUserGold();
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

        public async Task<int> GetUserGold()
        {
            return (await this.repository.Users.FirstOrDefaultAsync()).Gold;
        }
    }
}
