using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityAndAuth.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class renamed_name_property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "IdentityAndAuth",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "IdentityAndAuth",
                table: "Users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "IdentityAndAuth",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "IdentityAndAuth",
                table: "Users",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
