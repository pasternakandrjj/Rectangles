using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rectangles.Migrations
{
    public partial class RectangleUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "X",
                table: "Rectangles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Y",
                table: "Rectangles",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "X",
                table: "Rectangles");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "Rectangles");
        }
    }
}
