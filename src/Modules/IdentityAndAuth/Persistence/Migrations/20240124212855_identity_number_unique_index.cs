using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityAndAuth.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class identity_number_unique_index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "IdentityAndAuth",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Users_NationalIdentityNumber",
                schema: "IdentityAndAuth",
                table: "Users",
                column: "NationalIdentityNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_NationalIdentityNumber",
                schema: "IdentityAndAuth",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "IdentityAndAuth",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);
        }
    }
}
