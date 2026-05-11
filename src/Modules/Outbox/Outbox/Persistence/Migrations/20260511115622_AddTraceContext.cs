using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Outbox.Migrations
{
    /// <inheritdoc />
    public partial class AddTraceContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParentSpanId",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "character varying(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TraceId",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "character varying(32)",
                maxLength: 32,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentSpanId",
                schema: "Outbox",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "TraceId",
                schema: "Outbox",
                table: "OutboxMessages");
        }
    }
}
