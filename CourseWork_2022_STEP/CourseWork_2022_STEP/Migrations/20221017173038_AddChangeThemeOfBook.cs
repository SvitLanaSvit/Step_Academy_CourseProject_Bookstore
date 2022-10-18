using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWork_2022_STEP.Migrations
{
    public partial class AddChangeThemeOfBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThemeOfBookId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ThemeOfBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemeOfBooks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ThemeOfBookId",
                table: "Books",
                column: "ThemeOfBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ThemeOfBooks_ThemeOfBookId",
                table: "Books",
                column: "ThemeOfBookId",
                principalTable: "ThemeOfBooks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_ThemeOfBooks_ThemeOfBookId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "ThemeOfBooks");

            migrationBuilder.DropIndex(
                name: "IX_Books_ThemeOfBookId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ThemeOfBookId",
                table: "Books");
        }
    }
}
