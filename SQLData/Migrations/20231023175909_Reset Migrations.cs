using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQLData.Migrations
{
    /// <inheritdoc />
    public partial class ResetMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pfpUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    isVegan = table.Column<bool>(type: "bit", nullable: false),
                    isAlcoholFree = table.Column<bool>(type: "bit", nullable: false),
                    isVegatarian = table.Column<bool>(type: "bit", nullable: false),
                    isGlutenFree = table.Column<bool>(type: "bit", nullable: false),
                    isLactoseFree = table.Column<bool>(type: "bit", nullable: false),
                    isNutsFree = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    TakeOwnSnacks = table.Column<bool>(type: "bit", nullable: false),
                    AdultOnly = table.Column<bool>(type: "bit", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nights_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Is18Plus = table.Column<bool>(type: "bit", nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NightId = table.Column<int>(type: "int", nullable: true),
                    PersonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Nights_NightId",
                        column: x => x.NightId,
                        principalTable: "Nights",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Games_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NightPerson",
                columns: table => new
                {
                    NightsId = table.Column<int>(type: "int", nullable: false),
                    PlayersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NightPerson", x => new { x.NightsId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_NightPerson_Nights_NightsId",
                        column: x => x.NightsId,
                        principalTable: "Nights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NightPerson_Persons_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    writerId = table.Column<int>(type: "int", nullable: false),
                    organisatorId = table.Column<int>(type: "int", nullable: false),
                    nightId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Nights_nightId",
                        column: x => x.nightId,
                        principalTable: "Nights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Persons_organisatorId",
                        column: x => x.organisatorId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Persons_writerId",
                        column: x => x.writerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Snacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    personId = table.Column<int>(type: "int", nullable: false),
                    nightId = table.Column<int>(type: "int", nullable: true),
                    isVegan = table.Column<bool>(type: "bit", nullable: false),
                    isAlcoholFree = table.Column<bool>(type: "bit", nullable: false),
                    isVegatarian = table.Column<bool>(type: "bit", nullable: false),
                    isGlutenFree = table.Column<bool>(type: "bit", nullable: false),
                    isLactoseFree = table.Column<bool>(type: "bit", nullable: false),
                    isNutsFree = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Snacks_Nights_nightId",
                        column: x => x.nightId,
                        principalTable: "Nights",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Snacks_Persons_personId",
                        column: x => x.personId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_NightId",
                table: "Games",
                column: "NightId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PersonId",
                table: "Games",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_NightPerson_PlayersId",
                table: "NightPerson",
                column: "PlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_Nights_PersonId",
                table: "Nights",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_AddressId",
                table: "Persons",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_nightId",
                table: "Reviews",
                column: "nightId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_organisatorId",
                table: "Reviews",
                column: "organisatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_writerId",
                table: "Reviews",
                column: "writerId");

            migrationBuilder.CreateIndex(
                name: "IX_Snacks_nightId",
                table: "Snacks",
                column: "nightId");

            migrationBuilder.CreateIndex(
                name: "IX_Snacks_personId",
                table: "Snacks",
                column: "personId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "NightPerson");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Snacks");

            migrationBuilder.DropTable(
                name: "Nights");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
