using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWork_2022_STEP.Migrations
{
    public partial class AddChangeBookThemeOfBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_ThemeOfBooks_ThemeOfBookId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeOfBookId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ThemeOfBooks_ThemeOfBookId",
                table: "Books",
                column: "ThemeOfBookId",
                principalTable: "ThemeOfBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_ThemeOfBooks_ThemeOfBookId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeOfBookId",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ThemeOfBooks_ThemeOfBookId",
                table: "Books",
                column: "ThemeOfBookId",
                principalTable: "ThemeOfBooks",
                principalColumn: "Id");
        }
    }
}
