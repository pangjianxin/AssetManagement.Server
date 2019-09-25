using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boc.Assets.Infrastructure.migrations.eventstoredbcontextconfig
{
    public partial class ReplaceNonAuditEventWithStoredEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NonAuditEvents");

            migrationBuilder.CreateTable(
                name: "StoredEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MessageType = table.Column<string>(nullable: true),
                    AggregateId = table.Column<Guid>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoredEvents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoredEvents");

            migrationBuilder.CreateTable(
                name: "NonAuditEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Org2 = table.Column<string>(maxLength: 20, nullable: false),
                    OrgId = table.Column<Guid>(nullable: false),
                    OrgIdentifier = table.Column<string>(maxLength: 20, nullable: false),
                    OrgNam = table.Column<string>(maxLength: 50, nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonAuditEvents", x => x.Id);
                });
        }
    }
}
