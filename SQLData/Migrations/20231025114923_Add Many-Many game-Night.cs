using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQLData.Migrations
{
    /// <inheritdoc />
    public partial class AddManyManygameNight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Nights_NightId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_NightId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "NightId",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GameNight",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    NightsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameNight", x => new { x.GamesId, x.NightsId });
                    table.ForeignKey(
                        name: "FK_GameNight_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameNight_Nights_NightsId",
                        column: x => x.NightsId,
                        principalTable: "Nights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameNight_NightsId",
                table: "GameNight",
                column: "NightsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameNight");

            migrationBuilder.AddColumn<int>(
                name: "NightId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_NightId",
                table: "Games",
                column: "NightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Nights_NightId",
                table: "Games",
                column: "NightId",
                principalTable: "Nights",
                principalColumn: "Id");
        }
    }
}
