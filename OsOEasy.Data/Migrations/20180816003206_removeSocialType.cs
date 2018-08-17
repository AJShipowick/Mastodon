using Microsoft.EntityFrameworkCore.Migrations;

namespace OsOEasy.Data.Migrations
{
    public partial class removeSocialType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookImageType",
                table: "SocialSharing");

            migrationBuilder.DropColumn(
                name: "InstagramImageType",
                table: "SocialSharing");

            migrationBuilder.DropColumn(
                name: "LinkedinImageType",
                table: "SocialSharing");

            migrationBuilder.DropColumn(
                name: "PinterestImageType",
                table: "SocialSharing");

            migrationBuilder.DropColumn(
                name: "TwitterImageType",
                table: "SocialSharing");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookImageType",
                table: "SocialSharing",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramImageType",
                table: "SocialSharing",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedinImageType",
                table: "SocialSharing",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PinterestImageType",
                table: "SocialSharing",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterImageType",
                table: "SocialSharing",
                nullable: true);
        }
    }
}
