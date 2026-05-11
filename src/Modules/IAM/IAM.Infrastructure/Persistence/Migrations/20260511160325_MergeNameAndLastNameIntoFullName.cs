using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MergeNameAndLastNameIntoFullName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop GIN index and SearchVector generated column
            //    SearchVector references Name/LastName — must drop before those columns go
            //    Also avoids Npgsql's code-gen validation of OldAnnotation references
            migrationBuilder.DropIndex(
                name: "IX_Users_SearchVector",
                schema: "IAM",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                schema: "IAM",
                table: "Users");

            // 2. Add FullName (replaces Name + LastName)
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "IAM",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            // 3. Re-add SearchVector as generated column referencing FullName
            //    Raw SQL avoids NpgsqlMigrationsSqlGenerator tsvector validation
            migrationBuilder.Sql(
                """ALTER TABLE "IAM"."Users" ADD COLUMN "SearchVector" tsvector NULL GENERATED ALWAYS AS (to_tsvector('english'::regconfig, COALESCE("FullName", ''))) STORED;""");

            // 4. Re-create GIN index
            migrationBuilder.CreateIndex(
                name: "IX_Users_SearchVector",
                schema: "IAM",
                table: "Users",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            // 5. Drop old index and columns
            migrationBuilder.DropIndex(
                name: "IX_Users_NationalIdentityNumber",
                schema: "IAM",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NationalIdentityNumber",
                schema: "IAM",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                schema: "IAM",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "IAM",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. Drop GIN index and SearchVector generated column
            migrationBuilder.DropIndex(
                name: "IX_Users_SearchVector",
                schema: "IAM",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                schema: "IAM",
                table: "Users");

            // 2. Restore old columns
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "IAM",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                schema: "IAM",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NationalIdentityNumber",
                schema: "IAM",
                table: "Users",
                type: "character varying(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            // 3. Re-create SearchVector referencing Name + LastName
            //    Both columns exist at this point, so this is safe
            migrationBuilder.Sql(
                """ALTER TABLE "IAM"."Users" ADD COLUMN "SearchVector" tsvector NULL GENERATED ALWAYS AS (to_tsvector('english'::regconfig, COALESCE("Name", '') || ' ' || COALESCE("LastName", ''))) STORED;""");

            // 4. Re-create GIN index
            migrationBuilder.CreateIndex(
                name: "IX_Users_SearchVector",
                schema: "IAM",
                table: "Users",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            // 5. Re-create old unique index
            migrationBuilder.CreateIndex(
                name: "IX_Users_NationalIdentityNumber",
                schema: "IAM",
                table: "Users",
                column: "NationalIdentityNumber",
                unique: true);

            // 6. Drop FullName last
            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "IAM",
                table: "Users");
        }
    }
}
