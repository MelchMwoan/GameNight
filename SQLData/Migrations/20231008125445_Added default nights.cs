using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SQLData.Migrations
{
    /// <inheritdoc />
    public partial class Addeddefaultnights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Nights",
                columns: new[] { "Id", "DateTime", "MaxPlayers", "PersonId", "ThumbnailUrl", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Local), 3, 1, "https://s.yimg.com/ny/api/res/1.2/GjwW45jyFilXfDhM3Pl5rQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTEyMDA7aD02NzU-/https://media.zenfs.com/en/gobankingrates_644/4464a542bd2bf3c1300d1ae8de26441b", "Game Night" },
                    { 2, new DateTime(2023, 10, 11, 0, 0, 0, 0, DateTimeKind.Local), 8, 2, "https://s.yimg.com/ny/api/res/1.2/GjwW45jyFilXfDhM3Pl5rQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTEyMDA7aD02NzU-/https://media.zenfs.com/en/gobankingrates_644/4464a542bd2bf3c1300d1ae8de26441b", "Game Night" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Nights",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Nights",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
