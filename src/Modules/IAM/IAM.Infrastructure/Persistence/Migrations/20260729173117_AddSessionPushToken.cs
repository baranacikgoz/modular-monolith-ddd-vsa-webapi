using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IAM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionPushToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PushToken",
                schema: "IAM",
                table: "Sessions",
                type: "character varying(4096)",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PushTokenUpdatedOn",
                schema: "IAM",
                table: "Sessions",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PushToken",
                schema: "IAM",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "PushTokenUpdatedOn",
                schema: "IAM",
                table: "Sessions");
        }
    }
}
