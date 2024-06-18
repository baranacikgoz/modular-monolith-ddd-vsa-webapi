using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Common.Infrastructure.Persistence.Outbox.Migrations
{
    /// <inheritdoc />
    public partial class SwitchToDateTimeOffset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastFailedAt",
                schema: "Outbox",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "LastFailedAt",
                schema: "Outbox",
                table: "DeadLetterMessages");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ProcessedOn",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedOn",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedOn",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastFailedOn",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "LastModifiedOn",
                schema: "Outbox",
                table: "DeadLetterMessages",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedOn",
                schema: "Outbox",
                table: "DeadLetterMessages",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastFailedOn",
                schema: "Outbox",
                table: "DeadLetterMessages",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastFailedOn",
                schema: "Outbox",
                table: "OutboxMessages");

            migrationBuilder.DropColumn(
                name: "LastFailedOn",
                schema: "Outbox",
                table: "DeadLetterMessages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ProcessedOn",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastFailedAt",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastModifiedOn",
                schema: "Outbox",
                table: "DeadLetterMessages",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                schema: "Outbox",
                table: "DeadLetterMessages",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastFailedAt",
                schema: "Outbox",
                table: "DeadLetterMessages",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
