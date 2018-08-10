using Microsoft.EntityFrameworkCore.Migrations;

namespace OsOEasy.Data.Migrations
{
    public partial class addSideOfScreen2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SideOfScreen",
                table: "SocialSharing",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SideOfScreen",
                table: "SocialSharing");
        }
    }
}
