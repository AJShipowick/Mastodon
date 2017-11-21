using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OsOEasy.Migrations
{
    public partial class create2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PromotionEntries");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "PromotionEntries");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PromotionEntries",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "PromotionEntries");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "PromotionEntries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "PromotionEntries",
                nullable: true);
        }
    }
}
