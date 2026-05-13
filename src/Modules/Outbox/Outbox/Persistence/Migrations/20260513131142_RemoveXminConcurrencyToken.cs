using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Outbox.Migrations
{
    /// <inheritdoc />
    public partial class RemoveXminConcurrencyToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // xmin is a Postgres system column — no DDL needed; migration exists to update the EF model snapshot only.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No-op: xmin cannot be re-added via DDL.
        }
    }
}
