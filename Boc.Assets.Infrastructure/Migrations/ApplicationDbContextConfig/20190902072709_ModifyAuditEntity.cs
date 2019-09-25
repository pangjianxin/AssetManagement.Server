using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.applicationdbcontextconfig
{
    public partial class ModifyAuditEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastModifiedContent",
                table: "AssetReturns",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedContent",
                table: "AssetExchanges",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedContent",
                table: "AssetApplies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifiedContent",
                table: "AssetReturns");

            migrationBuilder.DropColumn(
                name: "LastModifiedContent",
                table: "AssetExchanges");

            migrationBuilder.DropColumn(
                name: "LastModifiedContent",
                table: "AssetApplies");
        }
    }
}
