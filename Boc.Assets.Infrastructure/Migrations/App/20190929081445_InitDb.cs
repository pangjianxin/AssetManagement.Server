using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.Migrations.app
{
    public partial class InitDb : Migration
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
                    TargetOrgId = table.Column<Guid>(nullable: false),
                    TargetOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    LastModifiedContent = table.Column<string>(nullable: true),
                    AssetCategoryId = table.Column<Guid>(nullable: false),
                    AssetCategoryThirdLevel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetApplies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetFirstLevelCategory = table.Column<string>(maxLength: 50, nullable: false),
                    AssetSecondLevelCategory = table.Column<string>(maxLength: 50, nullable: false),
                    AssetThirdLevelCategory = table.Column<string>(maxLength: 50, nullable: false),
                    AssetMeteringUnit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetDeploys",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetDeployCategory = table.Column<int>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    AssetId = table.Column<Guid>(nullable: false),
                    AssetTagNumber = table.Column<string>(maxLength: 100, nullable: true),
                    AssetName = table.Column<string>(maxLength: 100, nullable: false),
                    AssetNo = table.Column<string>(maxLength: 50, nullable: true),
                    ExportOrgInfo_OrgId = table.Column<Guid>(nullable: false),
                    ExportOrgInfo_OrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    ExportOrgInfo_OrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    ImportOrgInfo_OrgId = table.Column<Guid>(nullable: false),
                    ImportOrgInfo_OrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    ImportOrgInfo_OrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    AuthorizeOrgInfo_OrgId = table.Column<Guid>(nullable: false),
                    AuthorizeOrgInfo_OrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    AuthorizeOrgInfo_OrgNam = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetDeploys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetExchanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequestOrgId = table.Column<Guid>(nullable: false),
                    RequestOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    RequestOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    TargetOrgId = table.Column<Guid>(nullable: false),
                    TargetOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    LastModifiedContent = table.Column<string>(nullable: true),
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
                name: "AssetInventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PublisherId = table.Column<Guid>(nullable: false),
                    PublisherName = table.Column<string>(maxLength: 50, nullable: false),
                    PublisherIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    PublisherOrg2 = table.Column<string>(maxLength: 20, nullable: false),
                    TaskName = table.Column<string>(maxLength: 50, nullable: false),
                    TaskComment = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    ExpiryDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetInventories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetReturns",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequestOrgId = table.Column<Guid>(nullable: false),
                    RequestOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    RequestOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    TargetOrgId = table.Column<Guid>(nullable: false),
                    TargetOrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    TargetOrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    LastModifiedContent = table.Column<string>(nullable: true),
                    AssetId = table.Column<Guid>(nullable: false),
                    AssetName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetReturns", x => x.Id);
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
                name: "OrganizationRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationRoles", x => x.Id);
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
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                        name: "FK_Organizations_OrganizationRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "OrganizationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    ControllerName = table.Column<string>(maxLength: 20, nullable: false),
                    ActionName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_OrganizationRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "OrganizationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetInventoryRegisters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParticipationId = table.Column<Guid>(nullable: false),
                    AssetInventoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetInventoryRegisters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetInventoryRegisters_AssetInventories_AssetInventoryId",
                        column: x => x.AssetInventoryId,
                        principalTable: "AssetInventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetInventoryRegisters_Organizations_ParticipationId",
                        column: x => x.ParticipationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetCategoryId = table.Column<Guid>(nullable: true),
                    OrganizationInChargeId = table.Column<Guid>(nullable: true),
                    OrganizationInUseId = table.Column<Guid>(nullable: true),
                    AssetName = table.Column<string>(maxLength: 100, nullable: false),
                    SerialNumber = table.Column<string>(maxLength: 100, nullable: true),
                    Brand = table.Column<string>(maxLength: 50, nullable: true),
                    AssetDescription = table.Column<string>(maxLength: 100, nullable: true),
                    AssetType = table.Column<string>(maxLength: 100, nullable: true),
                    AssetTagNumber = table.Column<string>(maxLength: 50, nullable: true),
                    AssetNo = table.Column<string>(maxLength: 100, nullable: true),
                    AssetStatus = table.Column<int>(nullable: false),
                    InStoreDateTime = table.Column<DateTime>(nullable: true),
                    LastModifyDateTime = table.Column<DateTime>(nullable: true),
                    LastDeployRecord = table.Column<string>(maxLength: 100, nullable: true),
                    CreateDateTime = table.Column<DateTime>(nullable: true),
                    OrgInUseIdentifier = table.Column<string>(maxLength: 20, nullable: true),
                    OrgInUseName = table.Column<string>(maxLength: 50, nullable: true),
                    AssetLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_AssetCategories_AssetCategoryId",
                        column: x => x.AssetCategoryId,
                        principalTable: "AssetCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assets_Organizations_OrganizationInChargeId",
                        column: x => x.OrganizationInChargeId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assets_Organizations_OrganizationInUseId",
                        column: x => x.OrganizationInUseId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoryManageRegisters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetCategoryId = table.Column<Guid>(nullable: false),
                    ManagerId = table.Column<Guid>(nullable: false),
                    Org2 = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryManageRegisters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryManageRegisters_AssetCategories_AssetCategoryId",
                        column: x => x.AssetCategoryId,
                        principalTable: "AssetCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryManageRegisters_Organizations_ManagerId",
                        column: x => x.ManagerId,
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
                name: "AssetInventoryDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetInventoryRegisterId = table.Column<Guid>(nullable: false),
                    AssetId = table.Column<Guid>(nullable: false),
                    ResponsibilityIdentity = table.Column<string>(maxLength: 20, nullable: false),
                    ResponsibilityName = table.Column<string>(maxLength: 50, nullable: false),
                    ResponsibilityOrg2 = table.Column<string>(maxLength: 20, nullable: false),
                    AssetInventoryLocation = table.Column<string>(maxLength: 100, nullable: false),
                    InventoryStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetInventoryDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetInventoryDetails_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetInventoryDetails_AssetInventoryRegisters_AssetInventoryRegisterId",
                        column: x => x.AssetInventoryRegisterId,
                        principalTable: "AssetInventoryRegisters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetInventoryDetails_AssetId",
                table: "AssetInventoryDetails",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetInventoryDetails_AssetInventoryRegisterId",
                table: "AssetInventoryDetails",
                column: "AssetInventoryRegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetInventoryRegisters_AssetInventoryId",
                table: "AssetInventoryRegisters",
                column: "AssetInventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetInventoryRegisters_ParticipationId",
                table: "AssetInventoryRegisters",
                column: "ParticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetCategoryId",
                table: "Assets",
                column: "AssetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OrganizationInChargeId",
                table: "Assets",
                column: "OrganizationInChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OrganizationInUseId",
                table: "Assets",
                column: "OrganizationInUseId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryManageRegisters_AssetCategoryId",
                table: "CategoryManageRegisters",
                column: "AssetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryManageRegisters_ManagerId",
                table: "CategoryManageRegisters",
                column: "ManagerId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetApplies");

            migrationBuilder.DropTable(
                name: "AssetDeploys");

            migrationBuilder.DropTable(
                name: "AssetExchanges");

            migrationBuilder.DropTable(
                name: "AssetInventoryDetails");

            migrationBuilder.DropTable(
                name: "AssetReturns");

            migrationBuilder.DropTable(
                name: "CategoryManageRegisters");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Maintainers");

            migrationBuilder.DropTable(
                name: "OrganizationSpaces");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "AssetInventoryRegisters");

            migrationBuilder.DropTable(
                name: "AssetCategories");

            migrationBuilder.DropTable(
                name: "AssetInventories");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "OrganizationRoles");
        }
    }
}
