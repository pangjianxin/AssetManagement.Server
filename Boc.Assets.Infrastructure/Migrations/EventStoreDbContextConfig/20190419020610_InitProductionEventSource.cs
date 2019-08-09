using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.eventstoredbcontextconfig
{
    public partial class InitProductionEventSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventHandleMessage",
                table: "ApplyMaintainingEvents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventHandleMessage",
                table: "ApplyMaintainingEvents");
        }
    }
}
