using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StoreProductRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StoreProducts_ProductId",
                schema: "Inventory",
                table: "StoreProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreProducts_Products_ProductId",
                schema: "Inventory",
                table: "StoreProducts",
                column: "ProductId",
                principalSchema: "Inventory",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreProducts_Products_ProductId",
                schema: "Inventory",
                table: "StoreProducts");

            migrationBuilder.DropIndex(
                name: "IX_StoreProducts_ProductId",
                schema: "Inventory",
                table: "StoreProducts");
        }
    }
}
