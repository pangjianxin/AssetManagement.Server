using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.applicationdbcontextconfig
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetDeploys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetDeployCategory = table.Column<int>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    AssetTagNumber = table.Column<string>(maxLength: 100, nullable: true),
                    AssetName = table.Column<string>(maxLength: 100, nullable: false),
                    ExportOrgInfo_Org2 = table.Column<string>(nullable: true),
                    ExportOrgInfo_OrgId = table.Column<Guid>(nullable: false),
                    ExportOrgInfo_OrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    ExportOrgInfo_OrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    ImportOrgInfo_Org2 = table.Column<string>(nullable: true),
                    ImportOrgInfo_OrgId = table.Column<Guid>(nullable: false),
                    ImportOrgInfo_OrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    ImportOrgInfo_OrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    AuthorizeOrgInfo_Org2 = table.Column<string>(nullable: true),
                    AuthorizeOrgInfo_OrgId = table.Column<Guid>(nullable: false),
                    AuthorizeOrgInfo_OrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    AuthorizeOrgInfo_OrgNam = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetDeploys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetStockTakings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PublisherId = table.Column<Guid>(nullable: false),
                    PublisherName = table.Column<string>(maxLength: 50, nullable: false),
                    PublisherIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    PublisherOrg2 = table.Column<string>(maxLength: 20, nullable: false),
                    ManagementLineId = table.Column<Guid>(nullable: false),
                    ManagementLineName = table.Column<string>(maxLength: 20, nullable: false),
                    ManagementLineDescription = table.Column<string>(maxLength: 50, nullable: false),
                    TaskName = table.Column<string>(maxLength: 50, nullable: false),
                    TaskComment = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    ExpiryDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetStockTakings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Identifier = table.Column<string>(maxLength: 50, nullable: false),
                    Org2 = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(maxLength: 20, nullable: true),
                    OfficePhone = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ManagementLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ManagementLineName = table.Column<string>(maxLength: 50, nullable: false),
                    ManagementLineDescription = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagementLine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleNam = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ManagementLineId = table.Column<Guid>(nullable: false),
                    AssetFirstLevelCategory = table.Column<string>(maxLength: 50, nullable: false),
                    AssetSecondLevelCategory = table.Column<string>(maxLength: 50, nullable: false),
                    AssetThirdLevelCategory = table.Column<string>(maxLength: 50, nullable: false),
                    AssetMeteringUnit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetCategories_ManagementLine_ManagementLineId",
                        column: x => x.ManagementLineId,
                        principalTable: "ManagementLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ManagementLineId = table.Column<Guid>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: false),
                    OrgIdentifier = table.Column<string>(nullable: true),
                    OrgNam = table.Column<string>(maxLength: 100, nullable: false),
                    OrgShortNam = table.Column<string>(maxLength: 100, nullable: false),
                    UpOrg = table.Column<string>(maxLength: 10, nullable: false),
                    OrgLvl = table.Column<string>(maxLength: 10, nullable: false),
                    Org1 = table.Column<string>(maxLength: 10, nullable: false),
                    OrgNam1 = table.Column<string>(maxLength: 100, nullable: false),
                    Org2 = table.Column<string>(maxLength: 10, nullable: true),
                    OrgNam2 = table.Column<string>(maxLength: 100, nullable: true),
                    Org3 = table.Column<string>(maxLength: 10, nullable: true),
                    OrgNam3 = table.Column<string>(maxLength: 100, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_ManagementLine_ManagementLineId",
                        column: x => x.ManagementLineId,
                        principalTable: "ManagementLine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Organizations_OrganizationRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "OrganizationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maintainers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetCategoryId = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: false),
                    MaintainerName = table.Column<string>(maxLength: 20, nullable: false),
                    Telephone = table.Column<string>(maxLength: 20, nullable: false),
                    OfficePhone = table.Column<string>(maxLength: 20, nullable: true),
                    Org2 = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintainers_AssetCategories_AssetCategoryId",
                        column: x => x.AssetCategoryId,
                        principalTable: "AssetCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetCategoryId = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: true),
                    AssetLocation = table.Column<string>(nullable: true),
                    AssetName = table.Column<string>(maxLength: 100, nullable: false),
                    SerialNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Brand = table.Column<string>(maxLength: 50, nullable: false),
                    AssetDescription = table.Column<string>(maxLength: 100, nullable: true),
                    AssetType = table.Column<string>(maxLength: 100, nullable: true),
                    AssetTagNumber = table.Column<string>(maxLength: 50, nullable: true),
                    AssetNo = table.Column<string>(maxLength: 100, nullable: true),
                    AssetStatus = table.Column<int>(nullable: false),
                    InStoreDateTime = table.Column<DateTime>(nullable: true),
                    LastModifyDateTime = table.Column<DateTime>(nullable: true),
                    LastModifyComment = table.Column<string>(maxLength: 100, nullable: true),
                    CreateDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_AssetCategories_AssetCategoryId",
                        column: x => x.AssetCategoryId,
                        principalTable: "AssetCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assets_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AssetStockTakingOrganization",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    AssetStockTakingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetStockTakingOrganization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetStockTakingOrganization_AssetStockTakings_AssetStockTakingId",
                        column: x => x.AssetStockTakingId,
                        principalTable: "AssetStockTakings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetStockTakingOrganization_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationSpaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrgId = table.Column<Guid>(nullable: false),
                    OrgIdentifier = table.Column<string>(nullable: true),
                    OrgName = table.Column<string>(maxLength: 100, nullable: false),
                    SpaceName = table.Column<string>(maxLength: 100, nullable: false),
                    SpaceDescription = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationSpaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationSpaces_Organizations_OrgId",
                        column: x => x.OrgId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetStockTakingDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetStockTakingOrganizationId = table.Column<Guid>(nullable: false),
                    AssetId = table.Column<Guid>(nullable: false),
                    ResponsibilityIdentity = table.Column<string>(maxLength: 20, nullable: false),
                    ResponsibilityName = table.Column<string>(maxLength: 50, nullable: false),
                    ResponsibilityOrg2 = table.Column<string>(maxLength: 20, nullable: false),
                    AssetStockTakingLocation = table.Column<string>(maxLength: 100, nullable: false),
                    StockTakingStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetStockTakingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetStockTakingDetails_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetStockTakingDetails_AssetStockTakingOrganization_AssetStockTakingOrganizationId",
                        column: x => x.AssetStockTakingOrganizationId,
                        principalTable: "AssetStockTakingOrganization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetCategories_ManagementLineId",
                table: "AssetCategories",
                column: "ManagementLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetCategoryId",
                table: "Assets",
                column: "AssetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OrganizationId",
                table: "Assets",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetStockTakingDetails_AssetId",
                table: "AssetStockTakingDetails",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetStockTakingDetails_AssetStockTakingOrganizationId",
                table: "AssetStockTakingDetails",
                column: "AssetStockTakingOrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetStockTakingOrganization_OrganizationId",
                table: "AssetStockTakingOrganization",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetStockTakingOrganization_AssetStockTakingId_OrganizationId",
                table: "AssetStockTakingOrganization",
                columns: new[] { "AssetStockTakingId", "OrganizationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Identifier",
                table: "Employees",
                column: "Identifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Maintainers_AssetCategoryId",
                table: "Maintainers",
                column: "AssetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_ManagementLineId",
                table: "Organizations",
                column: "ManagementLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_OrgIdentifier",
                table: "Organizations",
                column: "OrgIdentifier",
                unique: true,
                filter: "[OrgIdentifier] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_RoleId",
                table: "Organizations",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationSpaces_OrgId",
                table: "OrganizationSpaces",
                column: "OrgId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetDeploys");

            migrationBuilder.DropTable(
                name: "AssetStockTakingDetails");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Maintainers");

            migrationBuilder.DropTable(
                name: "OrganizationSpaces");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "AssetStockTakingOrganization");

            migrationBuilder.DropTable(
                name: "AssetCategories");

            migrationBuilder.DropTable(
                name: "AssetStockTakings");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "ManagementLine");

            migrationBuilder.DropTable(
                name: "OrganizationRoles");
        }
    }
}
