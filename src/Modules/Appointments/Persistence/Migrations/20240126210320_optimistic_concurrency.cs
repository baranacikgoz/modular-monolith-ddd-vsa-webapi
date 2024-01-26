using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appointments.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class optimistic_concurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "Appointments",
                table: "Venues",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "Appointments",
                table: "Appointments",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "Appointments",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "Appointments",
                table: "Appointments");
        }
    }
}
