using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.Applicationdbcontextconfig
{
    public partial class ModifyOrgRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleNam",
                table: "OrganizationRoles");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "OrganizationRoles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "OrganizationRoles");

            migrationBuilder.AddColumn<string>(
                name: "RoleNam",
                table: "OrganizationRoles",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
