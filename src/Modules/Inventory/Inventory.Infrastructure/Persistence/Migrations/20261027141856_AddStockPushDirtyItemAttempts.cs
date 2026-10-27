using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStockPushDirtyItemAttempts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastErrorKey",
                schema: "Inventory",
                table: "StockPushDirtyItems",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PushAttempts",
                schema: "Inventory",
                table: "StockPushDirtyItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastErrorKey",
                schema: "Inventory",
                table: "StockPushDirtyItems");

            migrationBuilder.DropColumn(
                name: "PushAttempts",
                schema: "Inventory",
                table: "StockPushDirtyItems");
        }
    }
}
