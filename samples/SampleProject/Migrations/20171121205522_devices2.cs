using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SampleProject.Migrations
{
    public partial class devices2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDeviceTokens",
                table: "UserDeviceTokens");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserDeviceTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDeviceTokens",
                table: "UserDeviceTokens",
                columns: new[] { "UserId", "Token" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDeviceTokens",
                table: "UserDeviceTokens");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserDeviceTokens",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDeviceTokens",
                table: "UserDeviceTokens",
                column: "Token");
        }
    }
}
