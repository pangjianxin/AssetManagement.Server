using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.eventstoredbcontextconfig
{
    public partial class ModifyEventStoreDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventSubTitle",
                table: "NonAuditEvents");

            migrationBuilder.DropColumn(
                name: "EventTitle",
                table: "NonAuditEvents");

            migrationBuilder.DropColumn(
                name: "AggregateId",
                table: "AssetReturningEvents");

            migrationBuilder.DropColumn(
                name: "AggregateId",
                table: "AssetExchangingEvents");

            migrationBuilder.DropColumn(
                name: "AggregateId",
                table: "AssetApplyingEvents");

            migrationBuilder.RenameColumn(
                name: "AggregateId",
                table: "NonAuditEvents",
                newName: "OrgId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrgId",
                table: "NonAuditEvents",
                newName: "AggregateId");

            migrationBuilder.AddColumn<string>(
                name: "EventSubTitle",
                table: "NonAuditEvents",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EventTitle",
                table: "NonAuditEvents",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "AggregateId",
                table: "AssetReturningEvents",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AggregateId",
                table: "AssetExchangingEvents",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AggregateId",
                table: "AssetApplyingEvents",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
