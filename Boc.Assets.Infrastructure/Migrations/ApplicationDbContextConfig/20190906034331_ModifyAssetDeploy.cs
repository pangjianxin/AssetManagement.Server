using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.applicationdbcontextconfig
{
    public partial class ModifyAssetDeploy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizeOrgInfo_Org2",
                table: "AssetDeploys");

            migrationBuilder.DropColumn(
                name: "ExportOrgInfo_Org2",
                table: "AssetDeploys");

            migrationBuilder.RenameColumn(
                name: "ImportOrgInfo_Org2",
                table: "AssetDeploys",
                newName: "Org2");

            migrationBuilder.AlterColumn<string>(
                name: "Org2",
                table: "AssetDeploys",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                table: "AssetDeploys",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "AssetDeploys");

            migrationBuilder.RenameColumn(
                name: "Org2",
                table: "AssetDeploys",
                newName: "ImportOrgInfo_Org2");

            migrationBuilder.AlterColumn<string>(
                name: "ImportOrgInfo_Org2",
                table: "AssetDeploys",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "AuthorizeOrgInfo_Org2",
                table: "AssetDeploys",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExportOrgInfo_Org2",
                table: "AssetDeploys",
                nullable: true);
        }
    }
}
