using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Common.Persistence.TransactionalOutbox.Migrations
{
    /// <inheritdoc />
    public partial class made_messages_auditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TransactionalOutbox");

            migrationBuilder.CreateTable(
                name: "DeadLetterMessages",
                schema: "TransactionalOutbox",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Event = table.Column<string>(type: "text", nullable: false),
                    FailedCount = table.Column<int>(type: "integer", nullable: false),
                    LastFailedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedIp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeadLetterMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "TransactionalOutbox",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsProcessed = table.Column<bool>(type: "boolean", nullable: false),
                    ProcessedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Event = table.Column<string>(type: "text", nullable: false),
                    FailedCount = table.Column<int>(type: "integer", nullable: false),
                    LastFailedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedIp = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_CreatedOn",
                schema: "TransactionalOutbox",
                table: "OutboxMessages",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_IsProcessed",
                schema: "TransactionalOutbox",
                table: "OutboxMessages",
                column: "IsProcessed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeadLetterMessages",
                schema: "TransactionalOutbox");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "TransactionalOutbox");
        }
    }
}
