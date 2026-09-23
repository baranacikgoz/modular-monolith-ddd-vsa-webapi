using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Outbox.Migrations
{
    /// <inheritdoc />
    public partial class OutboxMessageIdBigint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxMessages_IsProcessed_FailedOn_NextRetryAt_CreatedOn",
                schema: "Outbox",
                table: "OutboxMessages");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_NextRetryAt_CreatedOn",
                schema: "Outbox",
                table: "OutboxMessages",
                columns: new[] { "NextRetryAt", "CreatedOn" },
                filter: "\"IsProcessed\" = false AND \"FailedOn\" IS NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutboxMessages_NextRetryAt_CreatedOn",
                schema: "Outbox",
                table: "OutboxMessages");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Outbox",
                table: "OutboxMessages",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_IsProcessed_FailedOn_NextRetryAt_CreatedOn",
                schema: "Outbox",
                table: "OutboxMessages",
                columns: new[] { "IsProcessed", "FailedOn", "NextRetryAt", "CreatedOn" });
        }
    }
}
