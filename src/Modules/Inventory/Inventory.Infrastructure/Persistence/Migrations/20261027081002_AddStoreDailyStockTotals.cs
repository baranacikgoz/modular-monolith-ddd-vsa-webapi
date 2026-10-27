using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreDailyStockTotals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreDailyStockTotals",
                schema: "Inventory",
                columns: table => new
                {
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    Day = table.Column<DateOnly>(type: "date", nullable: false),
                    CommittedReservations = table.Column<int>(type: "integer", nullable: false),
                    CommittedUnits = table.Column<long>(type: "bigint", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreDailyStockTotals", x => new { x.StoreId, x.Day });
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ReservationId",
                schema: "Inventory",
                table: "StockMovements",
                column: "ReservationId",
                unique: true,
                filter: "\"Type\" = 'ReservationCommitted'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreDailyStockTotals",
                schema: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_StockMovements_ReservationId",
                schema: "Inventory",
                table: "StockMovements");
        }
    }
}
