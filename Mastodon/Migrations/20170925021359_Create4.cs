using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Mastodon.Migrations
{
    public partial class Create4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdditionalMessage",
                table: "ClientsWebsites");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "ClientsWebsites");

            migrationBuilder.DropColumn(
                name: "MesasgeBody",
                table: "ClientsWebsites");

            migrationBuilder.AddColumn<string>(
                name: "CallToActionMessage",
                table: "ClientsWebsites",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormName",
                table: "ClientsWebsites",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallToActionMessage",
                table: "ClientsWebsites");

            migrationBuilder.DropColumn(
                name: "FormName",
                table: "ClientsWebsites");

            migrationBuilder.AddColumn<string>(
                name: "AdditionalMessage",
                table: "ClientsWebsites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "ClientsWebsites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MesasgeBody",
                table: "ClientsWebsites",
                nullable: true);
        }
    }
}
