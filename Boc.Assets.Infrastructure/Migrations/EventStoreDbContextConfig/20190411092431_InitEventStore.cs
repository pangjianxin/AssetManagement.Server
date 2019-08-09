using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.eventstoredbcontextconfig
{
    public partial class InitEventStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetApplyingEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AggregateId = table.Column<Guid>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    RequestOrgId = table.Column<Guid>(nullable: false),
                    RequestOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    RequestOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    Org2 = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgId = table.Column<Guid>(nullable: false),
                    TargetOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Message = table.Column<string>(maxLength: 50, nullable: false),
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
                    AggregateId = table.Column<Guid>(nullable: false),
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
                    AssetName = table.Column<string>(nullable: true),
                    ExchangeOrgId = table.Column<Guid>(nullable: false),
                    ExchangeOrgIdentifier = table.Column<string>(nullable: true),
                    ExchangeOrgNam = table.Column<string>(nullable: true)
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
                    AggregateId = table.Column<Guid>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    RequestOrgId = table.Column<Guid>(nullable: false),
                    RequestOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    RequestOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    Org2 = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgId = table.Column<Guid>(nullable: false),
                    TargetOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Message = table.Column<string>(maxLength: 50, nullable: false),
                    AssetId = table.Column<Guid>(nullable: false),
                    AssetName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetReturningEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NonAuditEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AggregateId = table.Column<Guid>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    OrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    OrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    Org2 = table.Column<string>(maxLength: 20, nullable: false),
                    EventTitle = table.Column<string>(maxLength: 20, nullable: false),
                    EventSubTitle = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonAuditEvents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetApplyingEvents");

            migrationBuilder.DropTable(
                name: "AssetExchangingEvents");

            migrationBuilder.DropTable(
                name: "AssetReturningEvents");

            migrationBuilder.DropTable(
                name: "NonAuditEvents");
        }
    }
}
