using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStockBulkAdjustJobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockBulkAdjustJobs",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientRequestId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    FileName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ObjectKey = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    Reason = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    LastProcessedRow = table.Column<int>(type: "integer", nullable: false),
                    TotalRows = table.Column<int>(type: "integer", nullable: false),
                    RowsAdjusted = table.Column<int>(type: "integer", nullable: false),
                    RowsRejected = table.Column<int>(type: "integer", nullable: false),
                    RowErrors = table.Column<string>(type: "jsonb", nullable: false, defaultValueSql: "'[]'::jsonb"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    RequestedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    QueuedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    StartedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    FinishedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    FailureKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Progress = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockBulkAdjustJobs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockBulkAdjustJobs_FinishedOn",
                schema: "Inventory",
                table: "StockBulkAdjustJobs",
                column: "FinishedOn");

            migrationBuilder.CreateIndex(
                name: "IX_StockBulkAdjustJobs_RequestedBy_QueuedOn",
                schema: "Inventory",
                table: "StockBulkAdjustJobs",
                columns: new[] { "RequestedBy", "QueuedOn" });

            migrationBuilder.CreateIndex(
                name: "IX_StockBulkAdjustJobs_Status_StartedOn",
                schema: "Inventory",
                table: "StockBulkAdjustJobs",
                columns: new[] { "Status", "StartedOn" });

            migrationBuilder.CreateIndex(
                name: "UX_StockBulkAdjustJobs_RequestedBy_Active",
                schema: "Inventory",
                table: "StockBulkAdjustJobs",
                column: "RequestedBy",
                unique: true,
                filter: "\"Status\" IN (0, 1)");

            migrationBuilder.CreateIndex(
                name: "UX_StockBulkAdjustJobs_RequestedBy_ClientRequestId",
                schema: "Inventory",
                table: "StockBulkAdjustJobs",
                columns: new[] { "RequestedBy", "ClientRequestId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockBulkAdjustJobs",
                schema: "Inventory");
        }
    }
}
