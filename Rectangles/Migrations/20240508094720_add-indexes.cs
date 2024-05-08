using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rectangles.Migrations
{
    public partial class addindexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rectangles_Height",
                table: "Rectangles",
                column: "Height");

            migrationBuilder.CreateIndex(
                name: "IX_Rectangles_Width",
                table: "Rectangles",
                column: "Width");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rectangles_Height",
                table: "Rectangles");

            migrationBuilder.DropIndex(
                name: "IX_Rectangles_Width",
                table: "Rectangles");
        }
    }
}
