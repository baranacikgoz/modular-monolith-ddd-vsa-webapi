using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationReleaseAttemptTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastReleaseAttemptOn",
                schema: "Inventory",
                table: "StockReservations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockReservations_LastReleaseAttemptOn",
                schema: "Inventory",
                table: "StockReservations",
                column: "LastReleaseAttemptOn",
                filter: "\"LastReleaseAttemptReference\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockReservations_LastReleaseAttemptOn",
                schema: "Inventory",
                table: "StockReservations");

            migrationBuilder.DropColumn(
                name: "LastReleaseAttemptOn",
                schema: "Inventory",
                table: "StockReservations");
        }
    }
}
