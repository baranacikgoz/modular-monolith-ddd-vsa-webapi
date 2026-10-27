using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWebhookDeliveries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebhookDeliveries",
                schema: "Inventory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConnectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ReservationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceivedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookDeliveries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WebhookDeliveries_ConnectionId_DeliveryId",
                schema: "Inventory",
                table: "WebhookDeliveries",
                columns: new[] { "ConnectionId", "DeliveryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebhookDeliveries_ReceivedOn",
                schema: "Inventory",
                table: "WebhookDeliveries",
                column: "ReceivedOn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebhookDeliveries",
                schema: "Inventory");
        }
    }
}
