using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.applicationdbcontextconfig
{
    public partial class BreakChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetApplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequestOrgId = table.Column<Guid>(nullable: false),
                    RequestOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    RequestOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    Org2 = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgId = table.Column<Guid>(nullable: false),
                    TargetOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    AssetCategoryId = table.Column<Guid>(nullable: false),
                    AssetCategoryThirdLevel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetApplies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetExchanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequestOrgId = table.Column<Guid>(nullable: false),
                    RequestOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    RequestOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    Org2 = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgId = table.Column<Guid>(nullable: false),
                    TargetOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    AssetId = table.Column<Guid>(nullable: false),
                    AssetName = table.Column<string>(nullable: true),
                    ExchangeOrgId = table.Column<Guid>(nullable: false),
                    ExchangeOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    ExchangeOrgNam = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetExchanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetReturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequestOrgId = table.Column<Guid>(nullable: false),
                    RequestOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    RequestOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    Org2 = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgId = table.Column<Guid>(nullable: false),
                    TargetOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    AssetId = table.Column<Guid>(nullable: false),
                    AssetName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetReturns", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetApplies");

            migrationBuilder.DropTable(
                name: "AssetExchanges");

            migrationBuilder.DropTable(
                name: "AssetReturns");
        }
    }
}
