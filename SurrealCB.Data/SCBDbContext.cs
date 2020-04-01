namespace SurrealCB.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        public DbSet<LevelBoost> LevelBoosts { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<EnemyNpc> Enemies { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Reward> Rewards { get; set; }
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

            builder.Entity<Card>().Property(p => p.AtkType).HasConversion(new EnumToStringConverter<AtkType>());
            builder.Entity<Card>().Property(p => p.Element).HasConversion(new EnumToStringConverter<Element>());
            builder.Entity<Card>().Property(p => p.Rarity).HasConversion(new EnumToStringConverter<Rarity>());
            builder.Entity<CardPassive>().Property(p => p.Passive).HasConversion(new EnumToStringConverter<Passive>());
        }
    }
}