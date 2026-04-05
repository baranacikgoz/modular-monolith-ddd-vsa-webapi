using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace Products.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFullTextSearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                schema: "Products",
                table: "Stores",
                type: "tsvector",
                nullable: true)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Name", "Description", "Address" });

            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                schema: "Products",
                table: "ProductTemplates",
                type: "tsvector",
                nullable: true)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Brand", "Model", "Color" });

            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                schema: "Products",
                table: "Products",
                type: "tsvector",
                nullable: true)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Name", "Description" });

            migrationBuilder.CreateIndex(
                name: "IX_Stores_SearchVector",
                schema: "Products",
                table: "Stores",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTemplates_SearchVector",
                schema: "Products",
                table: "ProductTemplates",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SearchVector",
                schema: "Products",
                table: "Products",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stores_SearchVector",
                schema: "Products",
                table: "Stores");

            migrationBuilder.DropIndex(
                name: "IX_ProductTemplates_SearchVector",
                schema: "Products",
                table: "ProductTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Products_SearchVector",
                schema: "Products",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                schema: "Products",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                schema: "Products",
                table: "ProductTemplates");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                schema: "Products",
                table: "Products");
        }
    }
}
