using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProductTemplateUniqueConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_StoreId_ProductTemplateId",
                schema: "Products",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreId",
                schema: "Products",
                table: "Products",
                column: "StoreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_StoreId",
                schema: "Products",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreId_ProductTemplateId",
                schema: "Products",
                table: "Products",
                columns: new[] { "StoreId", "ProductTemplateId" },
                unique: true);
        }
    }
}
