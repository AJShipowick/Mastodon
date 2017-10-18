using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Mastodon.Migrations
{
    public partial class create3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackgroundColor",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ButtonColor",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ShowCouponBorder",
                table: "Promotion",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "ButtonColor",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "ShowCouponBorder",
                table: "Promotion");
        }
    }
}
