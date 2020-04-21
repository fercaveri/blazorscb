using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurrealCB.Data.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 64, nullable: true),
                    LastName = table.Column<string>(maxLength: 64, nullable: true),
                    FullName = table.Column<string>(maxLength: 64, nullable: true),
                    Gold = table.Column<int>(nullable: false),
                    Exp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardBoosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardBoosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestTime = table.Column<DateTime>(nullable: false),
                    ResponseMillis = table.Column<long>(nullable: false),
                    StatusCode = table.Column<int>(nullable: false),
                    Method = table.Column<string>(nullable: false),
                    Path = table.Column<string>(maxLength: 2048, nullable: false),
                    QueryString = table.Column<string>(maxLength: 2048, nullable: true),
                    RequestBody = table.Column<string>(maxLength: 256, nullable: true),
                    ResponseBody = table.Column<string>(maxLength: 256, nullable: true),
                    IPAddress = table.Column<string>(maxLength: 45, nullable: true),
                    ApplicationUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiLogs_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: false),
                    When = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    SenderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    LastPageVisited = table.Column<string>(nullable: true),
                    IsNavOpen = table.Column<bool>(nullable: false),
                    IsNavMinified = table.Column<bool>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardPassives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Passive = table.Column<string>(nullable: false),
                    Param1 = table.Column<double>(nullable: false),
                    Param2 = table.Column<double>(nullable: false),
                    Param3 = table.Column<double>(nullable: false),
                    CardBoostId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPassives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardPassives_CardBoosts_CardBoostId",
                        column: x => x.CardBoostId,
                        principalTable: "CardBoosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatBoosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    CardBoostId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatBoosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatBoosts_CardBoosts_CardBoostId",
                        column: x => x.CardBoostId,
                        principalTable: "CardBoosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hp = table.Column<int>(nullable: false),
                    Atk = table.Column<int>(nullable: false),
                    Def = table.Column<int>(nullable: false),
                    Imm = table.Column<int>(nullable: false),
                    Spd = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Tier = table.Column<int>(nullable: false),
                    Rarity = table.Column<string>(nullable: false),
                    AtkType = table.Column<string>(nullable: false),
                    Element = table.Column<string>(nullable: false),
                    PassiveId = table.Column<int>(nullable: true),
                    Value = table.Column<int>(nullable: false),
                    BaseExp = table.Column<int>(nullable: false),
                    RuneSlots = table.Column<int>(nullable: false),
                    ImgSrc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_CardPassives_PassiveId",
                        column: x => x.PassiveId,
                        principalTable: "CardPassives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CardRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardRecipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardRecipes_Cards_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gold = table.Column<int>(nullable: false),
                    Exp = table.Column<int>(nullable: false),
                    CardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rewards_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Tier = table.Column<int>(nullable: false),
                    Rarity = table.Column<int>(nullable: false),
                    RewardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Rewards_RewardId",
                        column: x => x.RewardId,
                        principalTable: "Rewards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    MinLevel = table.Column<int>(nullable: false),
                    Difficult = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    CompletionRewardId = table.Column<int>(nullable: true),
                    SrcImg = table.Column<string>(nullable: true),
                    MapId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maps_Rewards_CompletionRewardId",
                        column: x => x.CompletionRewardId,
                        principalTable: "Rewards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maps_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemRecipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemRecipes_Items_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enemies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    CardCount = table.Column<int>(nullable: false),
                    RandomCards = table.Column<bool>(nullable: false),
                    RewardId = table.Column<int>(nullable: true),
                    MapId = table.Column<int>(nullable: false),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enemies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enemies_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enemies_Rewards_RewardId",
                        column: x => x.RewardId,
                        principalTable: "Rewards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MapRequiredEnemy",
                columns: table => new
                {
                    MapId = table.Column<int>(nullable: false),
                    EnemyId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapRequiredEnemy", x => new { x.MapId, x.EnemyId });
                    table.ForeignKey(
                        name: "FK_MapRequiredEnemy_Enemies_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "Enemies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MapRequiredEnemy_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentExp = table.Column<int>(nullable: false),
                    CardId = table.Column<int>(nullable: false),
                    EnemyNpcId = table.Column<int>(nullable: true),
                    ApplicationUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerCards_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerCards_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerCards_Enemies_EnemyNpcId",
                        column: x => x.EnemyNpcId,
                        principalTable: "Enemies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LevelBoosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hp = table.Column<int>(nullable: false),
                    Atk = table.Column<int>(nullable: false),
                    Def = table.Column<int>(nullable: false),
                    Imm = table.Column<int>(nullable: false),
                    Spd = table.Column<double>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    BoostId = table.Column<int>(nullable: true),
                    ImprovedName = table.Column<string>(nullable: true),
                    RequiredBoostId = table.Column<int>(nullable: true),
                    Cost = table.Column<int>(nullable: false),
                    CardId = table.Column<int>(nullable: true),
                    PlayerCardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelBoosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LevelBoosts_CardBoosts_BoostId",
                        column: x => x.BoostId,
                        principalTable: "CardBoosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LevelBoosts_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LevelBoosts_PlayerCards_PlayerCardId",
                        column: x => x.PlayerCardId,
                        principalTable: "PlayerCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LevelBoosts_LevelBoosts_RequiredBoostId",
                        column: x => x.RequiredBoostId,
                        principalTable: "LevelBoosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Runes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    BoostId = table.Column<int>(nullable: true),
                    Rarity = table.Column<string>(nullable: false),
                    Element = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    MinTier = table.Column<int>(nullable: false),
                    MaxTier = table.Column<int>(nullable: false),
                    ImgSrc = table.Column<string>(nullable: true),
                    PlayerCardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Runes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Runes_CardBoosts_BoostId",
                        column: x => x.BoostId,
                        principalTable: "CardBoosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Runes_PlayerCards_PlayerCardId",
                        column: x => x.PlayerCardId,
                        principalTable: "PlayerCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerRunes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuneId = table.Column<int>(nullable: false),
                    Rarity = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRunes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerRunes_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerRunes_Runes_RuneId",
                        column: x => x.RuneId,
                        principalTable: "Runes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RuneRecipes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuneRecipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RuneRecipes_Runes_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Runes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequiredItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    CardRecipeId = table.Column<int>(nullable: true),
                    ItemRecipeId = table.Column<int>(nullable: true),
                    LevelBoostId = table.Column<int>(nullable: true),
                    RuneRecipeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequiredItem_CardRecipes_CardRecipeId",
                        column: x => x.CardRecipeId,
                        principalTable: "CardRecipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequiredItem_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequiredItem_ItemRecipes_ItemRecipeId",
                        column: x => x.ItemRecipeId,
                        principalTable: "ItemRecipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequiredItem_LevelBoosts_LevelBoostId",
                        column: x => x.LevelBoostId,
                        principalTable: "LevelBoosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequiredItem_RuneRecipes_RuneRecipeId",
                        column: x => x.RuneRecipeId,
                        principalTable: "RuneRecipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiLogs_ApplicationUserId",
                table: "ApiLogs",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CardPassives_CardBoostId",
                table: "CardPassives",
                column: "CardBoostId");

            migrationBuilder.CreateIndex(
                name: "IX_CardRecipes_ResultId",
                table: "CardRecipes",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_PassiveId",
                table: "Cards",
                column: "PassiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Enemies_MapId",
                table: "Enemies",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Enemies_RewardId",
                table: "Enemies",
                column: "RewardId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemRecipes_ResultId",
                table: "ItemRecipes",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_RewardId",
                table: "Items",
                column: "RewardId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelBoosts_BoostId",
                table: "LevelBoosts",
                column: "BoostId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelBoosts_CardId",
                table: "LevelBoosts",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelBoosts_PlayerCardId",
                table: "LevelBoosts",
                column: "PlayerCardId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelBoosts_RequiredBoostId",
                table: "LevelBoosts",
                column: "RequiredBoostId");

            migrationBuilder.CreateIndex(
                name: "IX_MapRequiredEnemy_EnemyId",
                table: "MapRequiredEnemy",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_Maps_CompletionRewardId",
                table: "Maps",
                column: "CompletionRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_Maps_MapId",
                table: "Maps",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCards_ApplicationUserId",
                table: "PlayerCards",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCards_CardId",
                table: "PlayerCards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerCards_EnemyNpcId",
                table: "PlayerCards",
                column: "EnemyNpcId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRunes_ApplicationUserId",
                table: "PlayerRunes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRunes_RuneId",
                table: "PlayerRunes",
                column: "RuneId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredItem_CardRecipeId",
                table: "RequiredItem",
                column: "CardRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredItem_ItemId",
                table: "RequiredItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredItem_ItemRecipeId",
                table: "RequiredItem",
                column: "ItemRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredItem_LevelBoostId",
                table: "RequiredItem",
                column: "LevelBoostId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredItem_RuneRecipeId",
                table: "RequiredItem",
                column: "RuneRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rewards_CardId",
                table: "Rewards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_RuneRecipes_ResultId",
                table: "RuneRecipes",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Runes_BoostId",
                table: "Runes",
                column: "BoostId");

            migrationBuilder.CreateIndex(
                name: "IX_Runes_PlayerCardId",
                table: "Runes",
                column: "PlayerCardId");

            migrationBuilder.CreateIndex(
                name: "IX_StatBoosts_CardBoostId",
                table: "StatBoosts",
                column: "CardBoostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiLogs");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MapRequiredEnemy");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "PlayerRunes");

            migrationBuilder.DropTable(
                name: "RequiredItem");

            migrationBuilder.DropTable(
                name: "StatBoosts");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CardRecipes");

            migrationBuilder.DropTable(
                name: "ItemRecipes");

            migrationBuilder.DropTable(
                name: "LevelBoosts");

            migrationBuilder.DropTable(
                name: "RuneRecipes");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Runes");

            migrationBuilder.DropTable(
                name: "PlayerCards");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Enemies");

            migrationBuilder.DropTable(
                name: "Maps");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "CardPassives");

            migrationBuilder.DropTable(
                name: "CardBoosts");
        }
    }
}
