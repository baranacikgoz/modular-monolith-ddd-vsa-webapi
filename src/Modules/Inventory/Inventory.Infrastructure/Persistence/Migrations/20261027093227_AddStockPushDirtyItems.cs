using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStockPushDirtyItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "StockPushMarkVersions",
                schema: "Inventory");

            migrationBuilder.CreateTable(
                name: "StockPushDirtyItems",
                schema: "Inventory",
                columns: table => new
                {
                    StockLevelId = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    MarkVersion = table.Column<long>(type: "bigint", nullable: false),
                    MarkedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPushDirtyItems", x => x.StockLevelId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockPushDirtyItems_MarkedOn",
                schema: "Inventory",
                table: "StockPushDirtyItems",
                column: "MarkedOn");

            migrationBuilder.CreateIndex(
                name: "IX_StockPushDirtyItems_StoreId_MarkedOn",
                schema: "Inventory",
                table: "StockPushDirtyItems",
                columns: new[] { "StoreId", "MarkedOn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockPushDirtyItems",
                schema: "Inventory");

            migrationBuilder.DropSequence(
                name: "StockPushMarkVersions",
                schema: "Inventory");
        }
    }
}
