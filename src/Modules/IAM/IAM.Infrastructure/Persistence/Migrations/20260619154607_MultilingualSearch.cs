using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MultilingualSearch : Migration
    {
        // Database-global, idempotent: unaccent extension + accent-folding configs. Repeated per module
        // migration because cross-module apply order is not guaranteed and the Users vector depends on
        // simple_unaccent existing first. Configs lack IF NOT EXISTS and a duplicate CREATE raises
        // unique_violation (23505) on pg_ts_config, so each is guarded by an explicit catalog check.
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

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(CreateSearchInfraSql);

            // FullName is a proper noun → universal layer only, indexed language-neutral (simple_unaccent), never stemmed.
            RebuildVector(
                migrationBuilder,
                expression: """setweight(to_tsvector('simple_unaccent', coalesce("FullName", '')), 'A')""");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Restore the previous single-language english generated column.
            RebuildVector(
                migrationBuilder,
                expression: """to_tsvector('english'::regconfig, coalesce("FullName", ''))""");
            // Configs / extension are database-global infrastructure shared with other modules — leave them.
        }

        private static void RebuildVector(MigrationBuilder migrationBuilder, string expression)
        {
            migrationBuilder.DropIndex(name: "IX_Users_SearchVector", schema: "IAM", table: "Users");
            migrationBuilder.DropColumn(name: "SearchVector", schema: "IAM", table: "Users");

            migrationBuilder.Sql(
                $"""ALTER TABLE "IAM"."Users" ADD COLUMN "SearchVector" tsvector GENERATED ALWAYS AS ({expression}) STORED;""");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SearchVector",
                schema: "IAM",
                table: "Users",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }
    }
}
