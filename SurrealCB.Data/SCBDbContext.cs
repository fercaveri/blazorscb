namespace SurrealCB.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using SurrealCB.Data.Model;
    using SurrealCB.Data.Shared;

    public class SCBDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, IRepository<IEntity>
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
        public DbSet<StatBoost> StatBoosts { get; set; }
        public DbSet<LevelBoost> LevelBoosts { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<EnemyNpc> Enemies { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Rune> Runes { get; set; }
        public DbSet<PlayerRune> PlayerRunes { get; set; }
        public DbSet<CardRecipe> CardRecipes { get; set; }
        public DbSet<ItemRecipe> ItemRecipes { get; set; }
        public DbSet<RuneRecipe> RuneRecipes { get; set; }
        public DbSet<ApiLogItem> ApiLogs { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasOne(a => a.Profile)
                .WithOne(b => b.ApplicationUser)
                .HasForeignKey<UserProfile>(b => b.UserId);

            builder.Entity<Map>()
                .HasMany(a => a.Enemies);

            builder.Entity<PlayerCard>()
                .HasOne(a => a.Card)
                .WithMany()
                .HasForeignKey(a => a.CardId);

            builder.Entity<PlayerRune>()
                .HasOne(a => a.Rune)
                .WithMany()
                .HasForeignKey(a => a.RuneId);

            builder.Entity<LevelBoost>()
                .HasOne(x => x.RequiredBoost)
                .WithMany()
                .HasForeignKey(a => a.RequiredBoostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MapRequiredEnemy>()
                .HasKey(bc => new { bc.MapId, bc.EnemyId });
            builder.Entity<MapRequiredEnemy>()
                .HasOne(bc => bc.Enemy)
                .WithMany(b => b.RequiredToMaps)
                .HasForeignKey(bc => bc.EnemyId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<MapRequiredEnemy>()
                .HasOne(bc => bc.Map)
                .WithMany(b => b.RequiredEnemies)
                .HasForeignKey(bc => bc.MapId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Card>().Property(p => p.AtkType).HasConversion(new EnumToStringConverter<AtkType>());
            builder.Entity<Card>().Property(p => p.Element).HasConversion(new EnumToStringConverter<Element>());
            builder.Entity<Card>().Property(p => p.Rarity).HasConversion(new EnumToStringConverter<Rarity>());
            builder.Entity<CardPassive>().Property(p => p.Passive).HasConversion(new EnumToStringConverter<Passive>());
            builder.Entity<Map>().Property(p => p.Difficult).HasConversion(new EnumToStringConverter<MapDifficult>());
            builder.Entity<Map>().Property(p => p.Type).HasConversion(new EnumToStringConverter<GameType>());
            builder.Entity<Rune>().Property(p => p.Rarity).HasConversion(new EnumToStringConverter<Rarity>());
            builder.Entity<StatBoost>().Property(p => p.Type).HasConversion(new EnumToStringConverter<BoostType>());
        }

        public IQueryable<IEntity> GetAll()
        {
            return this.Set<IEntity>().AsNoTracking();
        }

        public async Task<IEntity> GetByIdAsync(int id)
        {
            return await this.Set<IEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(IEntity entity)
        {
            await this.Set<IEntity>().AddAsync(entity);
            await this.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEntity entity)
        {
            await this.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await this.Set<IEntity>().FindAsync(id);
            this.Set<IEntity>().Remove(entity);
            await this.SaveChangesAsync();
        }
    }
}