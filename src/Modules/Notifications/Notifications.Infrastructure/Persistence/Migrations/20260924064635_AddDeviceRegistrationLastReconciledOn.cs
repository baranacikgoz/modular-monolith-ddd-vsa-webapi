using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notifications.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceRegistrationLastReconciledOn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastReconciledOn",
                schema: "Notifications",
                table: "DeviceRegistrations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRegistrations_IsActive_LastReconciledOn",
                schema: "Notifications",
                table: "DeviceRegistrations",
                columns: new[] { "IsActive", "LastReconciledOn" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeviceRegistrations_IsActive_LastReconciledOn",
                schema: "Notifications",
                table: "DeviceRegistrations");

            migrationBuilder.DropColumn(
                name: "LastReconciledOn",
                schema: "Notifications",
                table: "DeviceRegistrations");
        }
    }
}
