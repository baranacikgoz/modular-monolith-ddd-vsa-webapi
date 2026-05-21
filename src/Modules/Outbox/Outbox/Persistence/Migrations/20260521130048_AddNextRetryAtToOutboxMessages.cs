using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Outbox.Migrations
{
    /// <inheritdoc />
    public partial class AddNextRetryAtToOutboxMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxMessages_IsProcessed_FailedOn_CreatedOn",
                schema: "Outbox",
                table: "OutboxMessages");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "NextRetryAt",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_IsProcessed_FailedOn_NextRetryAt_CreatedOn",
                schema: "Outbox",
                table: "OutboxMessages",
                columns: new[] { "IsProcessed", "FailedOn", "NextRetryAt", "CreatedOn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxMessages_IsProcessed_FailedOn_NextRetryAt_CreatedOn",
                schema: "Outbox",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "NextRetryAt",
                schema: "Outbox",
                table: "OutboxMessages");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_IsProcessed_FailedOn_CreatedOn",
                schema: "Outbox",
                table: "OutboxMessages",
                columns: new[] { "IsProcessed", "FailedOn", "CreatedOn" });
        }
    }
}
