using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.applicationdbcontextconfig
{
    public partial class fixed_bugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagementLineDescription",
                table: "AssetStockTakings");

            migrationBuilder.DropColumn(
                name: "ManagementLineId",
                table: "AssetStockTakings");

            migrationBuilder.DropColumn(
                name: "ManagementLineName",
                table: "AssetStockTakings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagementLineDescription",
                table: "AssetStockTakings",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ManagementLineId",
                table: "AssetStockTakings",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ManagementLineName",
                table: "AssetStockTakings",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
