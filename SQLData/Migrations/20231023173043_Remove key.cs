using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQLData.Migrations
{
    /// <inheritdoc />
    public partial class Removekey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Persons_organisatorId",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Persons_organisatorId",
                table: "Reviews",
                column: "organisatorId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Persons_organisatorId",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Persons_organisatorId",
                table: "Reviews",
                column: "organisatorId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
