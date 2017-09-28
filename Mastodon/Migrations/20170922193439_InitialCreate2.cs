using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mastodon.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueWebsiteKey",
                table: "ClientsWebsites");

            migrationBuilder.AddColumn<string>(
                name: "CustomSiteScript",
                table: "ClientsWebsites",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomSiteScript",
                table: "ClientsWebsites");

            migrationBuilder.AddColumn<int>(
                name: "UniqueWebsiteKey",
                table: "ClientsWebsites",
                nullable: false,
                defaultValue: 0);
        }
    }
}
