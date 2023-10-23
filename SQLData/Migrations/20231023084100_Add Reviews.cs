using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SQLData.Migrations
{
    /// <inheritdoc />
    public partial class AddReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Nights_NightId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Persons_WriterId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "WriterId",
                table: "Reviews",
                newName: "writerId");

            migrationBuilder.RenameColumn(
                name: "NightId",
                table: "Reviews",
                newName: "nightId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_WriterId",
                table: "Reviews",
                newName: "IX_Reviews_writerId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_NightId",
                table: "Reviews",
                newName: "IX_Reviews_nightId");

            migrationBuilder.AlterColumn<int>(
                name: "nightId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Nights_nightId",
                table: "Reviews",
                column: "nightId",
                principalTable: "Nights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Persons_writerId",
                table: "Reviews",
                column: "writerId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Nights_nightId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Persons_writerId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "writerId",
                table: "Reviews",
                newName: "WriterId");

            migrationBuilder.RenameColumn(
                name: "nightId",
                table: "Reviews",
                newName: "NightId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_writerId",
                table: "Reviews",
                newName: "IX_Reviews_WriterId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_nightId",
                table: "Reviews",
                newName: "IX_Reviews_NightId");

            migrationBuilder.AlterColumn<int>(
                name: "NightId",
                table: "Reviews",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Nights_NightId",
                table: "Reviews",
                column: "NightId",
                principalTable: "Nights",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Persons_WriterId",
                table: "Reviews",
                column: "WriterId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
