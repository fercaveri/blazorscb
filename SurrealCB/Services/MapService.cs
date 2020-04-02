using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurrealCB.Data;
using SurrealCB.Data.Model;

namespace SurrealCB.Server
{
    public interface IMapService
    {
        Task<List<Map>> GetAll();
    }
    public class MapService : IMapService
    {
        private readonly SCBDbContext repository;

        public MapService(SCBDbContext repository)
        {
            this.repository = repository;
        }

        public async Task<List<Map>> GetAll()
        {
            var maps = await this.repository.Maps.ToListAsync();
            return maps;
        }
        
    }
}
