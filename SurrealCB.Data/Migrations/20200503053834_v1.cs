using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurrealCB.Data.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActiveEffects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromPosition = table.Column<int>(nullable: false),
                    Passive = table.Column<int>(nullable: false),
                    Param1 = table.Column<double>(nullable: false),
                    Param2 = table.Column<double>(nullable: false),
                    Param3 = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveEffects", x => x.Id);
                });

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
                name: "CardPassives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Passive = table.Column<string>(nullable: false),
                    Param1 = table.Column<double>(nullable: false),
                    Param2 = table.Column<double>(nullable: false),
                    Param3 = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPassives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false),
                    ItemType = table.Column<string>(nullable: false),
                    Tier = table.Column<int>(nullable: false),
                    Rarity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gold = table.Column<int>(nullable: false),
                    Exp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.Id);
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
                name: "StatBoosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoostType = table.Column<string>(nullable: false),
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
                name: "PassiveBoost",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardPassiveId = table.Column<int>(nullable: false),
                    CardBoostId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassiveBoost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassiveBoost_CardBoosts_CardBoostId",
                        column: x => x.CardBoostId,
                        principalTable: "CardBoosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PassiveBoost_CardPassives_CardPassiveId",
                        column: x => x.CardPassiveId,
                        principalTable: "CardPassives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    GameType = table.Column<string>(nullable: false),
                    CompletionRewardId = table.Column<int>(nullable: true),
                    SrcImg = table.Column<string>(nullable: true)
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
                    BoostId = table.Column<int>(nullable: false),
                    ImprovedName = table.Column<string>(nullable: true),
                    RequiredBoostId = table.Column<int>(nullable: true),
                    Cost = table.Column<int>(nullable: false),
                    CardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LevelBoosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LevelBoosts_CardBoosts_BoostId",
                        column: x => x.BoostId,
                        principalTable: "CardBoosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LevelBoosts_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
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
                name: "MapRequiredEnemies",
                columns: table => new
                {
                    MapId = table.Column<int>(nullable: false),
                    EnemyId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapRequiredEnemies", x => new { x.MapId, x.EnemyId });
                    table.ForeignKey(
                        name: "FK_MapRequiredEnemies_Enemies_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "Enemies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MapRequiredEnemies_Maps_MapId",
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
                name: "ActiveLevelBoosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelBoostId = table.Column<int>(nullable: false),
                    PlayerCardId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveLevelBoosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveLevelBoosts_LevelBoosts_LevelBoostId",
                        column: x => x.LevelBoostId,
                        principalTable: "LevelBoosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveLevelBoosts_PlayerCards_PlayerCardId",
                        column: x => x.PlayerCardId,
                        principalTable: "PlayerCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rune",
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
                    table.PrimaryKey("PK_Rune", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rune_CardBoosts_BoostId",
                        column: x => x.BoostId,
                        principalTable: "CardBoosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rune_PlayerCards_PlayerCardId",
                        column: x => x.PlayerCardId,
                        principalTable: "PlayerCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayerRune",
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
                    table.PrimaryKey("PK_PlayerRune", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerRune_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayerRune_Rune_RuneId",
                        column: x => x.RuneId,
                        principalTable: "Rune",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveLevelBoosts_LevelBoostId",
                table: "ActiveLevelBoosts",
                column: "LevelBoostId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveLevelBoosts_PlayerCardId",
                table: "ActiveLevelBoosts",
                column: "PlayerCardId");

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
                name: "IX_LevelBoosts_BoostId",
                table: "LevelBoosts",
                column: "BoostId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelBoosts_CardId",
                table: "LevelBoosts",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_LevelBoosts_RequiredBoostId",
                table: "LevelBoosts",
                column: "RequiredBoostId");

            migrationBuilder.CreateIndex(
                name: "IX_MapRequiredEnemies_EnemyId",
                table: "MapRequiredEnemies",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_Maps_CompletionRewardId",
                table: "Maps",
                column: "CompletionRewardId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PassiveBoost_CardBoostId",
                table: "PassiveBoost",
                column: "CardBoostId");

            migrationBuilder.CreateIndex(
                name: "IX_PassiveBoost_CardPassiveId",
                table: "PassiveBoost",
                column: "CardPassiveId");

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
                name: "IX_PlayerRune_ApplicationUserId",
                table: "PlayerRune",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRune_RuneId",
                table: "PlayerRune",
                column: "RuneId");

            migrationBuilder.CreateIndex(
                name: "IX_Rune_BoostId",
                table: "Rune",
                column: "BoostId");

            migrationBuilder.CreateIndex(
                name: "IX_Rune_PlayerCardId",
                table: "Rune",
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
                name: "ActiveEffects");

            migrationBuilder.DropTable(
                name: "ActiveLevelBoosts");

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
                name: "Item");

            migrationBuilder.DropTable(
                name: "MapRequiredEnemies");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "PassiveBoost");

            migrationBuilder.DropTable(
                name: "PlayerRune");

            migrationBuilder.DropTable(
                name: "StatBoosts");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "LevelBoosts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Rune");

            migrationBuilder.DropTable(
                name: "CardBoosts");

            migrationBuilder.DropTable(
                name: "PlayerCards");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Enemies");

            migrationBuilder.DropTable(
                name: "CardPassives");

            migrationBuilder.DropTable(
                name: "Maps");

            migrationBuilder.DropTable(
                name: "Rewards");
        }
    }
}
