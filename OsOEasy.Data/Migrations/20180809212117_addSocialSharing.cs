using Microsoft.EntityFrameworkCore.Migrations;

namespace OsOEasy.Data.Migrations
{
    public partial class addSocialSharing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialSharing",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    ActivePromotion = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    UseFacebook = table.Column<bool>(nullable: false),
                    FacebookURL = table.Column<string>(nullable: true),
                    FacebookImageName = table.Column<string>(nullable: true),
                    FacebookImageType = table.Column<string>(nullable: true),
                    UseTwitter = table.Column<bool>(nullable: false),
                    TwitterURL = table.Column<string>(nullable: true),
                    TwitterImageName = table.Column<string>(nullable: true),
                    TwitterImageType = table.Column<string>(nullable: true),
                    UseInstagram = table.Column<bool>(nullable: false),
                    InstagramURL = table.Column<string>(nullable: true),
                    InstagramImageName = table.Column<string>(nullable: true),
                    InstagramImageType = table.Column<string>(nullable: true),
                    UseTwitch = table.Column<bool>(nullable: false),
                    TwitchURL = table.Column<string>(nullable: true),
                    TwitchImageName = table.Column<string>(nullable: true),
                    TwitchImageType = table.Column<string>(nullable: true),
                    UseLinkedin = table.Column<bool>(nullable: false),
                    LinkedinURL = table.Column<string>(nullable: true),
                    LinkedinImageName = table.Column<string>(nullable: true),
                    LinkedinImageType = table.Column<string>(nullable: true),
                    UsePinterest = table.Column<bool>(nullable: false),
                    PinterestURL = table.Column<string>(nullable: true),
                    PinterestImageName = table.Column<string>(nullable: true),
                    PinterestImageType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialSharing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialSharing_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialSharing_ApplicationUserId",
                table: "SocialSharing",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialSharing");
        }
    }
}
