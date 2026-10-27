using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStockMovementSequence : Migration
    {
        /// <summary>
        ///     Numbers the rows that existed before the column did: inside one stock level by creation time, then by id, which
        ///     is a stable order though not the exact write order of rows saved in the same instant. The counter on the stock
        ///     level then starts from the number of its rows, so the next movement continues the ledger. Runs before the unique
        ///     index exists, while every row still holds the column default.
        /// </summary>
        public const string BackfillSql = """
            UPDATE "Inventory"."StockMovements" AS movement
            SET "Sequence" = numbered."Position"
            FROM (
                SELECT "Id", ROW_NUMBER() OVER (PARTITION BY "StockLevelId" ORDER BY "CreatedOn", "Id") AS "Position"
                FROM "Inventory"."StockMovements"
            ) AS numbered
            WHERE movement."Id" = numbered."Id";

            UPDATE "Inventory"."StockLevels" AS stock
            SET "LastMovementSequence" = counted."Total"
            FROM (
                SELECT "StockLevelId", COUNT(*) AS "Total"
                FROM "Inventory"."StockMovements"
                GROUP BY "StockLevelId"
            ) AS counted
            WHERE stock."Id" = counted."StockLevelId";
            """;

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockMovements_StockLevelId_CreatedOn",
                schema: "Inventory",
                table: "StockMovements");

            migrationBuilder.AddColumn<long>(
                name: "Sequence",
                schema: "Inventory",
                table: "StockMovements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "LastMovementSequence",
                schema: "Inventory",
                table: "StockLevels",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.Sql(BackfillSql);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_StockLevelId_Sequence",
                schema: "Inventory",
                table: "StockMovements",
                columns: new[] { "StockLevelId", "Sequence" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StockMovements_StockLevelId_Sequence",
                schema: "Inventory",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "Sequence",
                schema: "Inventory",
                table: "StockMovements");

            migrationBuilder.DropColumn(
                name: "LastMovementSequence",
                schema: "Inventory",
                table: "StockLevels");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_StockLevelId_CreatedOn",
                schema: "Inventory",
                table: "StockMovements",
                columns: new[] { "StockLevelId", "CreatedOn" });
        }
    }
}
