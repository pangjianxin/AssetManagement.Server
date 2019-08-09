using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.eventstoredbcontextconfig
{
    public partial class AddApplyMaintaingEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TargetOrgNam",
                table: "AssetExchangingEvents",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TargetOrgIdentifier",
                table: "AssetExchangingEvents",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestOrgNam",
                table: "AssetExchangingEvents",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestOrgIdentifier",
                table: "AssetExchangingEvents",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Org2",
                table: "AssetExchangingEvents",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "AssetExchangingEvents",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExchangeOrgNam",
                table: "AssetExchangingEvents",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExchangeOrgIdentifier",
                table: "AssetExchangingEvents",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AssetName",
                table: "AssetExchangingEvents",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ApplyMaintainingEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    RequestOrgId = table.Column<Guid>(nullable: false),
                    RequestOrgIdentifier = table.Column<string>(nullable: true),
                    RequestOrgNam = table.Column<string>(nullable: true),
                    Org2 = table.Column<string>(nullable: true),
                    TargetOrgId = table.Column<Guid>(nullable: false),
                    TargetOrgIdentifier = table.Column<string>(nullable: true),
                    TargetOrgNam = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    AssetId = table.Column<Guid>(nullable: false),
                    AssetName = table.Column<string>(maxLength: 50, nullable: false),
                    AssetDescription = table.Column<string>(maxLength: 50, nullable: true),
                    AssetTagNumber = table.Column<string>(maxLength: 50, nullable: true),
                    MaintainerId = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: false),
                    MaintainerName = table.Column<string>(maxLength: 20, nullable: false),
                    MaintainerTelephone = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyMaintainingEvents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyMaintainingEvents");

            migrationBuilder.AlterColumn<string>(
                name: "TargetOrgNam",
                table: "AssetExchangingEvents",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "TargetOrgIdentifier",
                table: "AssetExchangingEvents",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "RequestOrgNam",
                table: "AssetExchangingEvents",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "RequestOrgIdentifier",
                table: "AssetExchangingEvents",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Org2",
                table: "AssetExchangingEvents",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "AssetExchangingEvents",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ExchangeOrgNam",
                table: "AssetExchangingEvents",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ExchangeOrgIdentifier",
                table: "AssetExchangingEvents",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "AssetName",
                table: "AssetExchangingEvents",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
