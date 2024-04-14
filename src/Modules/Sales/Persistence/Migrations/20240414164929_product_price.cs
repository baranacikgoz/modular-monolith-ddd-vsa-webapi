using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class product_price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price_Amount",
                schema: "Sales",
                table: "Products",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Price_Currency",
                schema: "Sales",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price_Amount",
                schema: "Sales",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price_Currency",
                schema: "Sales",
                table: "Products");
        }
    }
}
