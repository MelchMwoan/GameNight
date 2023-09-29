using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameNight2.Migrations
{
    /// <inheritdoc />
    public partial class Attributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Snacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NightId",
                table: "Snacks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NightId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Reviews",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "WriterId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Person",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Person",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NightId",
                table: "Person",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Nights",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MaxPlayers",
                table: "Nights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrganisatorId",
                table: "Nights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Genre",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Is18Plus",
                table: "Games",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NightId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HouseNumber",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Snacks_NightId",
                table: "Snacks",
                column: "NightId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_NightId",
                table: "Reviews",
                column: "NightId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_WriterId",
                table: "Reviews",
                column: "WriterId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_AddressId",
                table: "Person",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_NightId",
                table: "Person",
                column: "NightId");

            migrationBuilder.CreateIndex(
                name: "IX_Nights_OrganisatorId",
                table: "Nights",
                column: "OrganisatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_NightId",
                table: "Games",
                column: "NightId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerId",
                table: "Games",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Nights_NightId",
                table: "Games",
                column: "NightId",
                principalTable: "Nights",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Person_PlayerId",
                table: "Games",
                column: "PlayerId",
                principalTable: "Person",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nights_Person_OrganisatorId",
                table: "Nights",
                column: "OrganisatorId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Addresses_AddressId",
                table: "Person",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Nights_NightId",
                table: "Person",
                column: "NightId",
                principalTable: "Nights",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Nights_NightId",
                table: "Reviews",
                column: "NightId",
                principalTable: "Nights",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Person_WriterId",
                table: "Reviews",
                column: "WriterId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Snacks_Nights_NightId",
                table: "Snacks",
                column: "NightId",
                principalTable: "Nights",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Nights_NightId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Person_PlayerId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Nights_Person_OrganisatorId",
                table: "Nights");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Addresses_AddressId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Nights_NightId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Nights_NightId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Person_WriterId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Snacks_Nights_NightId",
                table: "Snacks");

            migrationBuilder.DropIndex(
                name: "IX_Snacks_NightId",
                table: "Snacks");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_NightId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_WriterId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Person_AddressId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_NightId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Nights_OrganisatorId",
                table: "Nights");

            migrationBuilder.DropIndex(
                name: "IX_Games_NightId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_PlayerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Snacks");

            migrationBuilder.DropColumn(
                name: "NightId",
                table: "Snacks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "NightId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "WriterId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "NightId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Nights");

            migrationBuilder.DropColumn(
                name: "MaxPlayers",
                table: "Nights");

            migrationBuilder.DropColumn(
                name: "OrganisatorId",
                table: "Nights");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Is18Plus",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "NightId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Addresses");
        }
    }
}
