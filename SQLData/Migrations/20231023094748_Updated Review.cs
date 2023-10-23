using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQLData.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "organisatorId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_organisatorId",
                table: "Reviews",
                column: "organisatorId");

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

            migrationBuilder.DropIndex(
                name: "IX_Reviews_organisatorId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "organisatorId",
                table: "Reviews");
        }
    }
}
