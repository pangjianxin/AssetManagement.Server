using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.app
{
    public partial class OptimizeCodes1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Organizations");

            migrationBuilder.AddColumn<byte[]>(
                name: "Hash",
                table: "Organizations",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "Organizations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Organizations");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Organizations",
                nullable: true);
        }
    }
}
