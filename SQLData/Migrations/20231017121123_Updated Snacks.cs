using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQLData.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSnacks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isAlcoholFree",
                table: "Snacks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isGlutenFree",
                table: "Snacks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isLactoseFree",
                table: "Snacks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isNutsFree",
                table: "Snacks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isVegan",
                table: "Snacks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isVegatarian",
                table: "Snacks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "personId",
                table: "Snacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isAlcoholFree",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isGlutenFree",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isLactoseFree",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isNutsFree",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isVegan",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isVegatarian",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Nights",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2023, 10, 24, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Nights",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BirthDate", "isAlcoholFree", "isGlutenFree", "isLactoseFree", "isNutsFree", "isVegan", "isVegatarian" },
                values: new object[] { new DateTime(2005, 10, 17, 0, 0, 0, 0, DateTimeKind.Local), false, false, false, false, false, false });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BirthDate", "isAlcoholFree", "isGlutenFree", "isLactoseFree", "isNutsFree", "isVegan", "isVegatarian" },
                values: new object[] { new DateTime(2000, 10, 17, 0, 0, 0, 0, DateTimeKind.Local), false, false, false, false, false, false });

            migrationBuilder.CreateIndex(
                name: "IX_Snacks_personId",
                table: "Snacks",
                column: "personId");

            migrationBuilder.AddForeignKey(
                name: "FK_Snacks_Persons_personId",
                table: "Snacks",
                column: "personId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Snacks_Persons_personId",
                table: "Snacks");

            migrationBuilder.DropIndex(
                name: "IX_Snacks_personId",
                table: "Snacks");

            migrationBuilder.DropColumn(
                name: "isAlcoholFree",
                table: "Snacks");

            migrationBuilder.DropColumn(
                name: "isGlutenFree",
                table: "Snacks");

            migrationBuilder.DropColumn(
                name: "isLactoseFree",
                table: "Snacks");

            migrationBuilder.DropColumn(
                name: "isNutsFree",
                table: "Snacks");

            migrationBuilder.DropColumn(
                name: "isVegan",
                table: "Snacks");

            migrationBuilder.DropColumn(
                name: "isVegatarian",
                table: "Snacks");

            migrationBuilder.DropColumn(
                name: "personId",
                table: "Snacks");

            migrationBuilder.DropColumn(
                name: "isAlcoholFree",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "isGlutenFree",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "isLactoseFree",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "isNutsFree",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "isVegan",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "isVegatarian",
                table: "Persons");

            migrationBuilder.UpdateData(
                table: "Nights",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2023, 10, 23, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Nights",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateTime",
                value: new DateTime(2023, 10, 19, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1,
                column: "BirthDate",
                value: new DateTime(2005, 10, 16, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2,
                column: "BirthDate",
                value: new DateTime(2000, 10, 16, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
