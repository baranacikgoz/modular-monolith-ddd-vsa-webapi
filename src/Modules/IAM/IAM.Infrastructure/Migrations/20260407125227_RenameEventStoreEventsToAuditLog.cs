using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameEventStoreEventsToAuditLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename table (preserves data)
            migrationBuilder.RenameTable(
                name: "EventStoreEvents",
                schema: "IAM",
                newName: "AuditLog",
                newSchema: "IAM");

            // Rename primary key constraint
            migrationBuilder.Sql(
                """ALTER TABLE "IAM"."AuditLog" RENAME CONSTRAINT "PK_EventStoreEvents" TO "PK_AuditLog";""");

            // Drop old single-column index and create new composite index for audit log queries
            migrationBuilder.DropIndex(
                name: "IX_EventStoreEvents_AggregateType",
                schema: "IAM",
                table: "AuditLog");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLog_AggregateId_AggregateType_CreatedOn",
                schema: "IAM",
                table: "AuditLog",
                columns: new[] { "AggregateId", "AggregateType", "CreatedOn" },
                descending: new[] { false, false, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuditLog_AggregateId_AggregateType_CreatedOn",
                schema: "IAM",
                table: "AuditLog");

            migrationBuilder.CreateIndex(
                name: "IX_EventStoreEvents_AggregateType",
                schema: "IAM",
                table: "AuditLog",
                column: "AggregateType");

            migrationBuilder.Sql(
                """ALTER TABLE "IAM"."AuditLog" RENAME CONSTRAINT "PK_AuditLog" TO "PK_EventStoreEvents";""");

            migrationBuilder.RenameTable(
                name: "AuditLog",
                schema: "IAM",
                newName: "EventStoreEvents",
                newSchema: "IAM");
        }
    }
}
