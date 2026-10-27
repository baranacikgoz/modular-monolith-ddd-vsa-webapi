using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductPackageDimensions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PackageHeightCm",
                schema: "Products",
                table: "Products",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PackageLengthCm",
                schema: "Products",
                table: "Products",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PackageWeightKg",
                schema: "Products",
                table: "Products",
                type: "numeric(10,3)",
                precision: 10,
                scale: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PackageWidthCm",
                schema: "Products",
                table: "Products",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageHeightCm",
                schema: "Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PackageLengthCm",
                schema: "Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PackageWeightKg",
                schema: "Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PackageWidthCm",
                schema: "Products",
                table: "Products");
        }
    }
}
