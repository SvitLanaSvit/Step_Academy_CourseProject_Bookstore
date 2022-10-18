using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWork_2022_STEP.Migrations
{
    public partial class AddChangeInDatabaseBuyedBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyedBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AmountOfBuy = table.Column<int>(type: "int", nullable: false),
                    DateTimeOfBuy = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyedBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyedBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyedBooks_BookId",
                table: "BuyedBooks",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyedBooks");
        }
    }
}
