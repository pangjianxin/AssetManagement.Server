using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.eventstoredbcontextconfig
{
    public partial class BreakChanges02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplyMaintainingEvents");

            migrationBuilder.DropTable(
                name: "AssetApplyingEvents");

            migrationBuilder.DropTable(
                name: "AssetExchangingEvents");

            migrationBuilder.DropTable(
                name: "AssetReturningEvents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplyMaintainingEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetDescription = table.Column<string>(maxLength: 50, nullable: true),
                    AssetId = table.Column<Guid>(nullable: false),
                    AssetName = table.Column<string>(maxLength: 50, nullable: false),
                    AssetTagNumber = table.Column<string>(maxLength: 50, nullable: true),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: false),
                    EventHandleMessage = table.Column<string>(nullable: true),
                    MaintainerId = table.Column<Guid>(nullable: false),
                    MaintainerName = table.Column<string>(maxLength: 20, nullable: false),
                    MaintainerTelephone = table.Column<string>(maxLength: 20, nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Org2 = table.Column<string>(nullable: true),
                    RequestOrgId = table.Column<Guid>(nullable: false),
                    RequestOrgIdentifier = table.Column<string>(nullable: true),
                    RequestOrgNam = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TargetOrgId = table.Column<Guid>(nullable: false),
                    TargetOrgIdentifier = table.Column<string>(nullable: true),
                    TargetOrgNam = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplyMaintainingEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetApplyingEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(maxLength: 50, nullable: false),
                    Org2 = table.Column<string>(maxLength: 20, nullable: false),
                    RequestOrgId = table.Column<Guid>(nullable: false),
                    RequestOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    RequestOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TargetOrgId = table.Column<Guid>(nullable: false),
                    TargetOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    AssetCategoryInfo_AssetCategoryId = table.Column<Guid>(nullable: false),
                    AssetCategoryInfo_AssetCategoryThirdLevel = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetApplyingEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetExchangingEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetId = table.Column<Guid>(nullable: false),
                    AssetName = table.Column<string>(maxLength: 50, nullable: false),
                    ExchangeOrgId = table.Column<Guid>(nullable: false),
                    ExchangeOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    ExchangeOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    Message = table.Column<string>(maxLength: 50, nullable: false),
                    Org2 = table.Column<string>(maxLength: 20, nullable: false),
                    RequestOrgId = table.Column<Guid>(nullable: false),
                    RequestOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    RequestOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TargetOrgId = table.Column<Guid>(nullable: false),
                    TargetOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetExchangingEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetReturningEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetId = table.Column<Guid>(nullable: false),
                    AssetName = table.Column<string>(maxLength: 50, nullable: false),
                    Message = table.Column<string>(maxLength: 50, nullable: false),
                    Org2 = table.Column<string>(maxLength: 20, nullable: false),
                    RequestOrgId = table.Column<Guid>(nullable: false),
                    RequestOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    RequestOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TargetOrgId = table.Column<Guid>(nullable: false),
                    TargetOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetReturningEvents", x => x.Id);
                });
        }
    }
}
