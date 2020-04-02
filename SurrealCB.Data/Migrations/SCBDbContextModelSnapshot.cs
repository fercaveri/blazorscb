﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurrealCB.Data;

namespace SurrealCB.Data.Migrations
{
    [DbContext(typeof(SCBDbContext))]
    partial class SCBDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SurrealCB.Data.Model.ApiLogItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid?>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IPAddress")
                        .HasColumnType("nvarchar(45)")
                        .HasMaxLength(45);

                    b.Property<string>("Method")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(2048)")
                        .HasMaxLength(2048);

                    b.Property<string>("QueryString")
                        .HasColumnType("nvarchar(2048)")
                        .HasMaxLength(2048);

                    b.Property<string>("RequestBody")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<DateTime>("RequestTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResponseBody")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<long>("ResponseMillis")
                        .HasColumnType("bigint");

                    b.Property<int>("StatusCode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("ApiLogs");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.Card", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Atk")
                        .HasColumnType("int");

                    b.Property<string>("AtkType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BaseExp")
                        .HasColumnType("int");

                    b.Property<int>("Def")
                        .HasColumnType("int");

                    b.Property<string>("Element")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Hp")
                        .HasColumnType("int");

                    b.Property<string>("ImgSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Imm")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PassiveId")
                        .HasColumnType("int");

                    b.Property<string>("Rarity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RuneSlots")
                        .HasColumnType("int");

                    b.Property<double>("Spd")
                        .HasColumnType("float");

                    b.Property<int>("Tier")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PassiveId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.CardBoost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Atk")
                        .HasColumnType("int");

                    b.Property<int?>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("Def")
                        .HasColumnType("int");

                    b.Property<int>("Hp")
                        .HasColumnType("int");

                    b.Property<int>("Imm")
                        .HasColumnType("int");

                    b.Property<int?>("PassiveId")
                        .HasColumnType("int");

                    b.Property<double>("Spd")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("PassiveId");

                    b.ToTable("CardBoosts");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.CardPassive", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<double>("Param1")
                        .HasColumnType("float");

                    b.Property<double>("Param2")
                        .HasColumnType("float");

                    b.Property<double>("Param3")
                        .HasColumnType("float");

                    b.Property<string>("Passive")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CardPassives");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.CardRecipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ResultId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResultId");

                    b.ToTable("CardRecipes");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.EnemyNpc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExpGain")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("MapId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RewardId")
                        .HasColumnType("int");

                    b.Property<int>("X")
                        .HasColumnType("int");

                    b.Property<int>("Y")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.HasIndex("RewardId");

                    b.ToTable("Enemies");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<int?>("RewardId")
                        .HasColumnType("int");

                    b.Property<int>("Tier")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RewardId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.ItemRecipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ResultId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResultId");

                    b.ToTable("ItemRecipes");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.LevelBoost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BoostId")
                        .HasColumnType("int");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<string>("ImprovedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerCardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoostId");

                    b.HasIndex("PlayerCardId");

                    b.ToTable("LevelBoosts");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.Map", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CompletionRewardId")
                        .HasColumnType("int");

                    b.Property<int>("Difficult")
                        .HasColumnType("int");

                    b.Property<int?>("MapId")
                        .HasColumnType("int");

                    b.Property<int>("MinLevel")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SrcImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompletionRewardId");

                    b.HasIndex("MapId");

                    b.ToTable("Maps");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.MapRequiredEnemy", b =>
                {
                    b.Property<int>("MapId")
                        .HasColumnType("int");

                    b.Property<int>("EnemyId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("MapId", "EnemyId");

                    b.HasIndex("EnemyId");

                    b.ToTable("MapRequiredEnemy");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid?>("SenderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("When")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.PlayerCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("CurrentExp")
                        .HasColumnType("int");

                    b.Property<int?>("EnemyNpcId")
                        .HasColumnType("int");

                    b.Property<string>("Owner")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("EnemyNpcId");

                    b.ToTable("PlayerCards");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.RequiredItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int?>("CardRecipeId")
                        .HasColumnType("int");

                    b.Property<int?>("ItemRecipeId")
                        .HasColumnType("int");

                    b.Property<int?>("LevelBoostId")
                        .HasColumnType("int");

                    b.Property<int?>("ObjId")
                        .HasColumnType("int");

                    b.Property<int?>("RuneRecipeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardRecipeId");

                    b.HasIndex("ItemRecipeId");

                    b.HasIndex("LevelBoostId");

                    b.HasIndex("ObjId");

                    b.HasIndex("RuneRecipeId");

                    b.ToTable("RequiredItem");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.Reward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("Exp")
                        .HasColumnType("int");

                    b.Property<int>("Gold")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("Rewards");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.Rune", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BoostId")
                        .HasColumnType("int");

                    b.Property<int>("Element")
                        .HasColumnType("int");

                    b.Property<int>("MaxTier")
                        .HasColumnType("int");

                    b.Property<int>("MinTier")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlayerCardId")
                        .HasColumnType("int");

                    b.Property<int>("Rarity")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoostId");

                    b.HasIndex("PlayerCardId");

                    b.ToTable("Runes");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.RuneRecipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ResultId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ResultId");

                    b.ToTable("RuneRecipes");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<bool>("IsNavMinified")
                        .HasColumnType("bit");

                    b.Property<bool>("IsNavOpen")
                        .HasColumnType("bit");

                    b.Property<string>("LastPageVisited")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.ApiLogItem", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.ApplicationUser", null)
                        .WithMany("ApiLogItems")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.Card", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.CardPassive", "Passive")
                        .WithMany()
                        .HasForeignKey("PassiveId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.CardBoost", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.Card", null)
                        .WithMany("LevelBoosts")
                        .HasForeignKey("CardId");

                    b.HasOne("SurrealCB.Data.Model.CardPassive", "Passive")
                        .WithMany()
                        .HasForeignKey("PassiveId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.CardRecipe", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.Card", "Result")
                        .WithMany()
                        .HasForeignKey("ResultId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.EnemyNpc", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.Map", "Map")
                        .WithMany("Enemies")
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SurrealCB.Data.Model.Reward", "Reward")
                        .WithMany()
                        .HasForeignKey("RewardId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.Item", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.Reward", null)
                        .WithMany("Items")
                        .HasForeignKey("RewardId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.ItemRecipe", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.Item", "Result")
                        .WithMany()
                        .HasForeignKey("ResultId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.LevelBoost", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.CardBoost", "Boost")
                        .WithMany()
                        .HasForeignKey("BoostId");

                    b.HasOne("SurrealCB.Data.Model.PlayerCard", null)
                        .WithMany("ActiveLvlBoosts")
                        .HasForeignKey("PlayerCardId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.Map", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.Reward", "CompletionReward")
                        .WithMany()
                        .HasForeignKey("CompletionRewardId");

                    b.HasOne("SurrealCB.Data.Model.Map", null)
                        .WithMany("RequiredMaps")
                        .HasForeignKey("MapId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.MapRequiredEnemy", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.EnemyNpc", "Enemy")
                        .WithMany("RequiredToMaps")
                        .HasForeignKey("EnemyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SurrealCB.Data.Model.Map", "Map")
                        .WithMany("RequiredEnemies")
                        .HasForeignKey("MapId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SurrealCB.Data.Model.Message", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.ApplicationUser", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.PlayerCard", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SurrealCB.Data.Model.EnemyNpc", null)
                        .WithMany("Cards")
                        .HasForeignKey("EnemyNpcId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.RequiredItem", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.CardRecipe", null)
                        .WithMany("RequiredItems")
                        .HasForeignKey("CardRecipeId");

                    b.HasOne("SurrealCB.Data.Model.ItemRecipe", null)
                        .WithMany("RequiredItems")
                        .HasForeignKey("ItemRecipeId");

                    b.HasOne("SurrealCB.Data.Model.LevelBoost", null)
                        .WithMany("RequiredItems")
                        .HasForeignKey("LevelBoostId");

                    b.HasOne("SurrealCB.Data.Model.Item", "Obj")
                        .WithMany()
                        .HasForeignKey("ObjId");

                    b.HasOne("SurrealCB.Data.Model.RuneRecipe", null)
                        .WithMany("RequiredItems")
                        .HasForeignKey("RuneRecipeId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.Reward", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.Card", "Card")
                        .WithMany()
                        .HasForeignKey("CardId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.Rune", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.CardBoost", "Boost")
                        .WithMany()
                        .HasForeignKey("BoostId");

                    b.HasOne("SurrealCB.Data.Model.PlayerCard", null)
                        .WithMany("Runes")
                        .HasForeignKey("PlayerCardId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.RuneRecipe", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.Rune", "Result")
                        .WithMany()
                        .HasForeignKey("ResultId");
                });

            modelBuilder.Entity("SurrealCB.Data.Model.UserProfile", b =>
                {
                    b.HasOne("SurrealCB.Data.Model.ApplicationUser", "ApplicationUser")
                        .WithOne("Profile")
                        .HasForeignKey("SurrealCB.Data.Model.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
