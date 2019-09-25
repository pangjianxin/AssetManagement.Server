using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.applicationdbcontextconfig
{
    public partial class add_categoryOrgRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagementLineId",
                table: "Organizations");

            migrationBuilder.CreateTable(
                name: "CategoryOrgRegistrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetCategoryId = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    Org2 = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryOrgRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryOrgRegistrations_AssetCategories_AssetCategoryId",
                        column: x => x.AssetCategoryId,
                        principalTable: "AssetCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryOrgRegistrations_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryOrgRegistrations_AssetCategoryId",
                table: "CategoryOrgRegistrations",
                column: "AssetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryOrgRegistrations_OrganizationId",
                table: "CategoryOrgRegistrations",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryOrgRegistrations");

            migrationBuilder.AddColumn<Guid>(
                name: "ManagementLineId",
                table: "Organizations",
                nullable: true);
        }
    }
}
