using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Mastodon.Migrations
{
    public partial class Create5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SliderImagePath",
                table: "ClientsWebsites");

            migrationBuilder.AddColumn<string>(
                name: "SliderImageName",
                table: "ClientsWebsites",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SliderImageName",
                table: "ClientsWebsites");

            migrationBuilder.AddColumn<string>(
                name: "SliderImagePath",
                table: "ClientsWebsites",
                nullable: true);
        }
    }
}
