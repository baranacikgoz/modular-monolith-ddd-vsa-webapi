using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class made_events_auditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                schema: "Sales",
                table: "EventStoreEvents",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifiedBy",
                schema: "Sales",
                table: "EventStoreEvents",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedIp",
                schema: "Sales",
                table: "EventStoreEvents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Sales",
                table: "EventStoreEvents",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "Sales",
                table: "EventStoreEvents");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "Sales",
                table: "EventStoreEvents");

            migrationBuilder.DropColumn(
                name: "LastModifiedIp",
                schema: "Sales",
                table: "EventStoreEvents");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                schema: "Sales",
                table: "EventStoreEvents");
        }
    }
}
