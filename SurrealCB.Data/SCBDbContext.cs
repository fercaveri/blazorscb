namespace SurrealCB.Data
{
    using Microsoft.EntityFrameworkCore;
    using SurrealCB.Data.Model;
    using SurrealCB.Data.Shared;

    public class SCBDbContext : DbContext
    {
        private readonly IUserSession _userSession;

        public SCBDbContext(DbContextOptions<SCBDbContext> options)
            : base(options)
        {
        }

        public SCBDbContext(DbContextOptions<SCBDbContext> options, IUserSession userSession) : base(options)
        {
            _userSession = userSession;
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<PlayerCard> PlayerCards { get; set; }
        public DbSet<CardPassive> CardPassives { get; set; }
        public DbSet<CardBoost> CardBoosts { get; set; }

        public DbSet<ApiLogItem> ApiLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}