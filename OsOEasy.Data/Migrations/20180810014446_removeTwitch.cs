using Microsoft.EntityFrameworkCore.Migrations;

namespace OsOEasy.Data.Migrations
{
    public partial class removeTwitch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwitchImageName",
                table: "SocialSharing");

            migrationBuilder.DropColumn(
                name: "TwitchImageType",
                table: "SocialSharing");

            migrationBuilder.DropColumn(
                name: "TwitchURL",
                table: "SocialSharing");

            migrationBuilder.DropColumn(
                name: "UseTwitch",
                table: "SocialSharing");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TwitchImageName",
                table: "SocialSharing",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitchImageType",
                table: "SocialSharing",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitchURL",
                table: "SocialSharing",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UseTwitch",
                table: "SocialSharing",
                nullable: false,
                defaultValue: false);
        }
    }
}
