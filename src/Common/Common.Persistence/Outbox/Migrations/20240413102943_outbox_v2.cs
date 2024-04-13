using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.Persistence.Outbox.Migrations
{
    /// <inheritdoc />
    public partial class outbox_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payload",
                schema: "Outbox",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "Payload",
                schema: "Outbox",
                table: "DeadLetterMessages");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "Outbox",
                table: "OutboxMessages",
                newName: "Event");

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "Outbox",
                table: "DeadLetterMessages",
                newName: "Event");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Event",
                schema: "Outbox",
                table: "OutboxMessages",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Event",
                schema: "Outbox",
                table: "DeadLetterMessages",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "Payload",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Payload",
                schema: "Outbox",
                table: "DeadLetterMessages",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
