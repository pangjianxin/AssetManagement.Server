using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.applicationdbcontextconfig
{
    public partial class remove_managementline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetCategories_ManagementLine_ManagementLineId",
                table: "AssetCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Organizations_OrganizationId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_ManagementLine_ManagementLineId",
                table: "Organizations");

            migrationBuilder.DropTable(
                name: "ManagementLine");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_ManagementLineId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_AssetCategories_ManagementLineId",
                table: "AssetCategories");

            migrationBuilder.DropColumn(
                name: "ManagementLineId",
                table: "AssetCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationBelongedId",
                table: "Assets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoredOrgIdentifier",
                table: "Assets",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StoredOrgName",
                table: "Assets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OrganizationBelongedId",
                table: "Assets",
                column: "OrganizationBelongedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Organizations_OrganizationBelongedId",
                table: "Assets",
                column: "OrganizationBelongedId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Organizations_OrganizationId",
                table: "Assets",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Organizations_OrganizationBelongedId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Organizations_OrganizationId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_OrganizationBelongedId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "OrganizationBelongedId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "StoredOrgIdentifier",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "StoredOrgName",
                table: "Assets");

            migrationBuilder.AddColumn<Guid>(
                name: "ManagementLineId",
                table: "AssetCategories",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ManagementLine",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    ManagementLineDescription = table.Column<string>(maxLength: 50, nullable: false),
                    ManagementLineName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagementLine", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_ManagementLineId",
                table: "Organizations",
                column: "ManagementLineId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetCategories_ManagementLineId",
                table: "AssetCategories",
                column: "ManagementLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetCategories_ManagementLine_ManagementLineId",
                table: "AssetCategories",
                column: "ManagementLineId",
                principalTable: "ManagementLine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Organizations_OrganizationId",
                table: "Assets",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_ManagementLine_ManagementLineId",
                table: "Organizations",
                column: "ManagementLineId",
                principalTable: "ManagementLine",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
