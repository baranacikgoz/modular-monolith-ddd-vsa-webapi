using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityAndAuth.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class refactored_aggregate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Version",
                schema: "IdentityAndAuth",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "EventStoreEvents",
                schema: "IdentityAndAuth",
                columns: table => new
                {
                    AggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    Event = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStoreEvents", x => new { x.AggregateId, x.Version });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventStoreEvents",
                schema: "IdentityAndAuth");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "IdentityAndAuth",
                table: "Users");
        }
    }
}
