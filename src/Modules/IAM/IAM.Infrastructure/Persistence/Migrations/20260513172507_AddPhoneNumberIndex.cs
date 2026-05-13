using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                schema: "IAM",
                table: "Users",
                column: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_PhoneNumber",
                schema: "IAM",
                table: "Users");
        }
    }
}
