using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityAndAuth.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class identity_role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "IdentityAndAuth",
                table: "Roles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "IdentityAndAuth",
                table: "Roles",
                type: "text",
                nullable: true);
        }
    }
}
