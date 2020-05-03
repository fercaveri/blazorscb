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
    public interface IMapService
    {
        Task<List<Map>> GetAll();
        Task<Map> GetById(int id);
    }
    public class MapService : IMapService
    {
        private readonly IRepository repository;

        public MapService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Map>> GetAll()
        {
            var maps = await this.repository.Query<Map>().ToListAsync();
            return maps;
        }
        
        public async Task<Map> GetById(int id)
        {
            var map = await this.repository.Query<Map>().FirstOrDefaultAsync(x => x.Id == id);
            return map;
        }
    }
}
