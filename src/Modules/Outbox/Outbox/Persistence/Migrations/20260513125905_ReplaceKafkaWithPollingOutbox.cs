using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Outbox.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceKafkaWithPollingOutbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxMessages_CreatedOn_IsProcessed",
                schema: "Outbox",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "EventType",
                schema: "Outbox",
                table: "OutboxMessages");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "FailedOn",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RetryCount",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_IsProcessed_FailedOn_CreatedOn",
                schema: "Outbox",
                table: "OutboxMessages",
                columns: new[] { "IsProcessed", "FailedOn", "CreatedOn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxMessages_IsProcessed_FailedOn_CreatedOn",
                schema: "Outbox",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "FailedOn",
                schema: "Outbox",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "RetryCount",
                schema: "Outbox",
                table: "OutboxMessages");

            migrationBuilder.AddColumn<string>(
                name: "EventType",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_CreatedOn_IsProcessed",
                schema: "Outbox",
                table: "OutboxMessages",
                columns: new[] { "CreatedOn", "IsProcessed" });
        }
    }
}
