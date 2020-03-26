namespace SurrealCB.Data
{
    using Microsoft.EntityFrameworkCore;
    using SurrealCB.Data.Model;

    public class SCBDbContext : DbContext
    {
        public SCBDbContext(DbContextOptions<SCBDbContext> options)
            : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<PlayerCard> PlayerCards { get; set; }
        public DbSet<CardPassive> CardPassives { get; set; }
        public DbSet<CardBoost> CardBoosts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}