using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SQLData.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDummyData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Snacks_Nights_NightId",
                table: "Snacks");

            migrationBuilder.DeleteData(
                table: "Nights",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Nights",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "NightId",
                table: "Snacks",
                newName: "nightId");

            migrationBuilder.RenameIndex(
                name: "IX_Snacks_NightId",
                table: "Snacks",
                newName: "IX_Snacks_nightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Snacks_Nights_nightId",
                table: "Snacks",
                column: "nightId",
                principalTable: "Nights",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Snacks_Nights_nightId",
                table: "Snacks");

            migrationBuilder.RenameColumn(
                name: "nightId",
                table: "Snacks",
                newName: "NightId");

            migrationBuilder.RenameIndex(
                name: "IX_Snacks_nightId",
                table: "Snacks",
                newName: "IX_Snacks_NightId");

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "HouseNumber", "Street" },
                values: new object[] { 1, "Breda", 63, "Lovensdijkstraat" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "AddressId", "BirthDate", "Email", "Gender", "Name", "RealName", "isAlcoholFree", "isGlutenFree", "isLactoseFree", "isNutsFree", "isVegan", "isVegatarian", "pfpUrl" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2005, 10, 17, 0, 0, 0, 0, DateTimeKind.Local), "henk@mail.nl", 77, "Henk", "Henk Man", false, false, false, false, false, false, "https://t4.ftcdn.net/jpg/00/64/67/27/360_F_64672736_U5kpdGs9keUll8CRQ3p3YaEv2M6qkVY5.jpg" },
                    { 2, 1, new DateTime(2000, 10, 17, 0, 0, 0, 0, DateTimeKind.Local), "jan@mail.nl", 88, "Jan", "Jan Man", false, false, false, false, false, false, "https://t4.ftcdn.net/jpg/00/64/67/27/360_F_64672736_U5kpdGs9keUll8CRQ3p3YaEv2M6qkVY5.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Nights",
                columns: new[] { "Id", "AdultOnly", "DateTime", "Description", "MaxPlayers", "PersonId", "TakeOwnSnacks", "ThumbnailUrl", "Title" },
                values: new object[,]
                {
                    { 1, false, new DateTime(2023, 10, 24, 0, 0, 0, 0, DateTimeKind.Local), null, 3, 1, true, "https://s.yimg.com/ny/api/res/1.2/GjwW45jyFilXfDhM3Pl5rQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTEyMDA7aD02NzU-/https://media.zenfs.com/en/gobankingrates_644/4464a542bd2bf3c1300d1ae8de26441b", "Game Night" },
                    { 2, false, new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Local), null, 8, 2, true, "https://s.yimg.com/ny/api/res/1.2/GjwW45jyFilXfDhM3Pl5rQ--/YXBwaWQ9aGlnaGxhbmRlcjt3PTEyMDA7aD02NzU-/https://media.zenfs.com/en/gobankingrates_644/4464a542bd2bf3c1300d1ae8de26441b", "Game Night" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Snacks_Nights_NightId",
                table: "Snacks",
                column: "NightId",
                principalTable: "Nights",
                principalColumn: "Id");
        }
    }
}
