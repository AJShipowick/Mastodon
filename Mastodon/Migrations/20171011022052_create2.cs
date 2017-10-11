using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Mastodon.Migrations
{
    public partial class create2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "PromotionDetails",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Promotion");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details1",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details2",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details3",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details4",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details5",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discount",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinePrint",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Promotion",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "Details1",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "Details2",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "Details3",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "Details4",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "Details5",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "FinePrint",
                table: "Promotion");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Promotion");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Promotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromotionDetails",
                table: "Promotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "Promotion",
                nullable: true);
        }
    }
}
