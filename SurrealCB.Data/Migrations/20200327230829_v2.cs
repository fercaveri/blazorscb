using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurrealCB.Data.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weather");

            migrationBuilder.CreateTable(
                name: "CardPassives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Passive = table.Column<int>(nullable: false),
                    Param1 = table.Column<string>(nullable: true),
                    Param2 = table.Column<string>(nullable: true),
                    Param3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPassives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Tier = table.Column<int>(nullable: false),
                    Rarity = table.Column<int>(nullable: false),
                    AtkType = table.Column<int>(nullable: false),
                    Element = table.Column<int>(nullable: false),
                    PassiveId = table.Column<int>(nullable: true),
                    Hp = table.Column<int>(nullable: false),
                    Atk = table.Column<int>(nullable: false),
                    Def = table.Column<int>(nullable: false),
                    Spd = table.Column<double>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    BaseExp = table.Column<int>(nullable: false),
                    RuneSlots = table.Column<int>(nullable: false),
                    ImgSrc = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Owner = table.Column<string>(nullable: true),
                    CurrentExp = table.Column<int>(nullable: true)
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
                name: "CardBoosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hp = table.Column<int>(nullable: false),
                    Atk = table.Column<int>(nullable: false),
                    Def = table.Column<int>(nullable: false),
                    Spd = table.Column<double>(nullable: false),
                    PassiveId = table.Column<int>(nullable: true),
                    CardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardBoosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardBoosts_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardBoosts_CardPassives_PassiveId",
                        column: x => x.PassiveId,
                        principalTable: "CardPassives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rune",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    BoostId = table.Column<int>(nullable: true),
                    Rarity = table.Column<int>(nullable: false),
                    Element = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    MinTier = table.Column<int>(nullable: false),
                    MaxTier = table.Column<int>(nullable: false),
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
                        name: "FK_Rune_Cards_PlayerCardId",
                        column: x => x.PlayerCardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardBoosts_CardId",
                table: "CardBoosts",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardBoosts_PassiveId",
                table: "CardBoosts",
                column: "PassiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_PassiveId",
                table: "Cards",
                column: "PassiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Rune_BoostId",
                table: "Rune",
                column: "BoostId");

            migrationBuilder.CreateIndex(
                name: "IX_Rune_PlayerCardId",
                table: "Rune",
                column: "PlayerCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rune");

            migrationBuilder.DropTable(
                name: "CardBoosts");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "CardPassives");

            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemperatureC = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                });
        }
    }
}
