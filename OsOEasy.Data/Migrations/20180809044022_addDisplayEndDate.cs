using Microsoft.EntityFrameworkCore.Migrations;

namespace OsOEasy.Data.Migrations
{
    public partial class addDisplayEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DisplayEndDate",
                table: "Promotion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayEndDate",
                table: "Promotion");
        }
    }
}
