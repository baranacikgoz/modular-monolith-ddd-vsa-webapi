using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MultilingualSearch : Migration
    {
        // Database-global, idempotent: unaccent extension + accent-folding text-search configs.
        // Text-search configs have no IF NOT EXISTS, and a duplicate CREATE raises unique_violation (23505)
        // on pg_ts_config — NOT duplicate_object — so each is guarded by an explicit catalog existence check.
        // Repeated per module migration because cross-module apply order is not guaranteed and every
        // module's vector depends on simple_unaccent existing first.
        private const string CreateSearchInfraSql = """
            CREATE EXTENSION IF NOT EXISTS unaccent;

            DO $$ BEGIN
                IF NOT EXISTS (SELECT 1 FROM pg_ts_config WHERE cfgname = 'simple_unaccent') THEN
                    CREATE TEXT SEARCH CONFIGURATION simple_unaccent ( COPY = simple );
                    ALTER TEXT SEARCH CONFIGURATION simple_unaccent
                        ALTER MAPPING FOR hword, hword_part, word WITH unaccent, simple;
                END IF;
            END $$;

            DO $$ BEGIN
                IF NOT EXISTS (SELECT 1 FROM pg_ts_config WHERE cfgname = 'english_unaccent') THEN
                    CREATE TEXT SEARCH CONFIGURATION english_unaccent ( COPY = english );
                    ALTER TEXT SEARCH CONFIGURATION english_unaccent
                        ALTER MAPPING FOR hword, hword_part, word WITH unaccent, english_stem;
                END IF;
            END $$;

            DO $$ BEGIN
                IF NOT EXISTS (SELECT 1 FROM pg_ts_config WHERE cfgname = 'turkish_unaccent') THEN
                    CREATE TEXT SEARCH CONFIGURATION turkish_unaccent ( COPY = turkish );
                    ALTER TEXT SEARCH CONFIGURATION turkish_unaccent
                        ALTER MAPPING FOR hword, hword_part, word WITH unaccent, turkish_stem;
                END IF;
            END $$;
            """;

        // IMMUTABLE wrappers: the per-row lang::regconfig cast is only STABLE, so a generated column
        // cannot use it directly — it must go through a function declared IMMUTABLE.
        private const string CreateWrapperFunctionsSql = """
            CREATE OR REPLACE FUNCTION fts_product(lang text, name text, descr text)
            RETURNS tsvector LANGUAGE sql IMMUTABLE AS $$
              SELECT setweight(to_tsvector('simple_unaccent', coalesce(name,  '')), 'A')
                  || setweight(to_tsvector(lang::regconfig,   coalesce(descr, '')), 'B');
            $$;

            CREATE OR REPLACE FUNCTION fts_store(lang text, name text, address text, descr text)
            RETURNS tsvector LANGUAGE sql IMMUTABLE AS $$
              SELECT setweight(to_tsvector('simple_unaccent', coalesce(name,    '')), 'A')
                  || setweight(to_tsvector('simple_unaccent', coalesce(address, '')), 'B')
                  || setweight(to_tsvector(lang::regconfig,   coalesce(descr,   '')), 'C');
            $$;
            """;

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Extension, accent-folding configs, immutable wrapper functions — before any generated column.
            migrationBuilder.Sql(CreateSearchInfraSql);
            migrationBuilder.Sql(CreateWrapperFunctionsSql);

            // 2. Per-row authored-language columns — must exist before the computed columns that reference them.
            migrationBuilder.AddColumn<string>(
                name: "Language",
                schema: "Products",
                table: "Stores",
                type: "text",
                nullable: false,
                defaultValue: "simple_unaccent");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                schema: "Products",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "simple_unaccent");

            // 3. Replace each single-language english generated column with the new two-layer / universal vector.
            //    Drop index → drop column → re-add generated column (raw SQL) → re-create GIN index.
            RebuildVector(
                migrationBuilder,
                table: "Stores",
                indexName: "IX_Stores_SearchVector",
                expression: """fts_store("Language", "Name", "Address", "Description")""");

            RebuildVector(
                migrationBuilder,
                table: "ProductTemplates",
                indexName: "IX_ProductTemplates_SearchVector",
                expression: """setweight(to_tsvector('simple_unaccent', coalesce("Brand",'') || ' ' || coalesce("Model",'') || ' ' || coalesce("Color",'')), 'A')""");

            RebuildVector(
                migrationBuilder,
                table: "Products",
                indexName: "IX_Products_SearchVector",
                expression: """fts_product("Language", "Name", "Description")""");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Restore the previous single-language english generated columns.
            RebuildVector(
                migrationBuilder,
                table: "Stores",
                indexName: "IX_Stores_SearchVector",
                expression: """to_tsvector('english'::regconfig, coalesce("Name", '') || ' ' || coalesce("Description", '') || ' ' || coalesce("Address", ''))""");

            RebuildVector(
                migrationBuilder,
                table: "ProductTemplates",
                indexName: "IX_ProductTemplates_SearchVector",
                expression: """to_tsvector('english'::regconfig, coalesce("Brand", '') || ' ' || coalesce("Model", '') || ' ' || coalesce("Color", ''))""");

            RebuildVector(
                migrationBuilder,
                table: "Products",
                indexName: "IX_Products_SearchVector",
                expression: """to_tsvector('english'::regconfig, coalesce("Name", '') || ' ' || coalesce("Description", ''))""");

            migrationBuilder.DropColumn(name: "Language", schema: "Products", table: "Stores");
            migrationBuilder.DropColumn(name: "Language", schema: "Products", table: "Products");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS fts_product(text, text, text);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS fts_store(text, text, text, text);");
            // Configs / extension are database-global infrastructure shared with other modules — leave them.
        }

        // Drops the GIN index and SearchVector column, re-adds SearchVector as a STORED generated column with the
        // given expression, then re-creates the GIN index. Raw ADD COLUMN avoids Npgsql's tsvector code-gen.
        private static void RebuildVector(MigrationBuilder migrationBuilder, string table, string indexName, string expression)
        {
            migrationBuilder.DropIndex(name: indexName, schema: "Products", table: table);
            migrationBuilder.DropColumn(name: "SearchVector", schema: "Products", table: table);

            migrationBuilder.Sql(
                $"""ALTER TABLE "Products"."{table}" ADD COLUMN "SearchVector" tsvector GENERATED ALWAYS AS ({expression}) STORED;""");

            migrationBuilder.CreateIndex(
                name: indexName,
                schema: "Products",
                table: table,
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }
    }
}
