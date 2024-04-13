using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityAndAuth.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class made_events_auditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "IdentityAndAuth",
                table: "EventStoreEvents",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "IdentityAndAuth",
                table: "EventStoreEvents",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedIp",
                schema: "IdentityAndAuth",
                table: "EventStoreEvents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "IdentityAndAuth",
                table: "EventStoreEvents",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "IdentityAndAuth",
                table: "EventStoreEvents");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "IdentityAndAuth",
                table: "EventStoreEvents");

            migrationBuilder.DropColumn(
                name: "LastModifiedIp",
                schema: "IdentityAndAuth",
                table: "EventStoreEvents");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "IdentityAndAuth",
                table: "EventStoreEvents");
        }
    }
}
