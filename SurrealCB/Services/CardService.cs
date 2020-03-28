using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurrealCB.Data;
using SurrealCB.Data.Model;

namespace SurrealCB.Server
{
    public interface ICardService
    {
        Task<List<Card>> GetAll();
    }
    public class CardService : ICardService
    {
        private readonly SCBDbContext repository;

        public CardService(SCBDbContext repository)
        {
            this.repository = repository;
        }

        public async Task<List<Card>> GetAll()
        {
            var cards = await this.repository.Cards.ToListAsync();
            return cards;
        }
    }
}
