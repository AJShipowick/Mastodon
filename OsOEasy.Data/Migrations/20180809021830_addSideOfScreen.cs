using Microsoft.EntityFrameworkCore.Migrations;

namespace OsOEasy.Data.Migrations
{
    public partial class addSideOfScreen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SideOfScreen",
                table: "Promotion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SideOfScreen",
                table: "Promotion");
        }
    }
}
