using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseWork_2022_STEP.Migrations
{
    public partial class AddChangeDeferredBookDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeferredBook",
                table: "DeferredBooks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDeferredBook",
                table: "DeferredBooks");
        }
    }
}
