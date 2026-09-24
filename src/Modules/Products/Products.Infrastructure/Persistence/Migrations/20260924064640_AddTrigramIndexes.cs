using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTrigramIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pg_trgm", ",,");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_Address",
                schema: "Products",
                table: "Stores",
                column: "Address")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Stores_Name",
                schema: "Products",
                table: "Stores",
                column: "Name")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTemplates_Brand",
                schema: "Products",
                table: "ProductTemplates",
                column: "Brand")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTemplates_Color",
                schema: "Products",
                table: "ProductTemplates",
                column: "Color")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTemplates_Model",
                schema: "Products",
                table: "ProductTemplates",
                column: "Model")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                schema: "Products",
                table: "Products",
                column: "Name")
                .Annotation("Npgsql:IndexMethod", "gin")
                .Annotation("Npgsql:IndexOperators", new[] { "gin_trgm_ops" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stores_Address",
                schema: "Products",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_Stores_Name",
                schema: "Products",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_ProductTemplates_Brand",
                schema: "Products",
                table: "ProductTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ProductTemplates_Color",
                schema: "Products",
                table: "ProductTemplates");

            migrationBuilder.DropIndex(
                name: "IX_ProductTemplates_Model",
                schema: "Products",
                table: "ProductTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                schema: "Products",
                table: "Products");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:PostgresExtension:pg_trgm", ",,");
        }
    }
}
