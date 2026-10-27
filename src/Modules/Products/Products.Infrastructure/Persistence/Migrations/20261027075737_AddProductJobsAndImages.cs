using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductJobsAndImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExportJobs",
                schema: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: true),
                    FileName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    ObjectKey = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    RequestedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    QueuedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    StartedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    FinishedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    FailureKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Progress = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImportJobs",
                schema: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientRequestId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    FileName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ObjectKey = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    LastProcessedRow = table.Column<int>(type: "integer", nullable: false),
                    TotalRows = table.Column<int>(type: "integer", nullable: false),
                    ProductsCreated = table.Column<int>(type: "integer", nullable: false),
                    ProductsUpdated = table.Column<int>(type: "integer", nullable: false),
                    RowsRejected = table.Column<int>(type: "integer", nullable: false),
                    RowErrors = table.Column<string>(type: "jsonb", nullable: false, defaultValueSql: "'[]'::jsonb"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    RequestedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    QueuedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    StartedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    FinishedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    FailureKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Progress = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportJobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                schema: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ObjectKey = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ContentType = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    SizeBytes = table.Column<long>(type: "bigint", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Products",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExportJobs_FinishedOn",
                schema: "Products",
                table: "ExportJobs",
                column: "FinishedOn");

            migrationBuilder.CreateIndex(
                name: "IX_ExportJobs_RequestedBy_QueuedOn",
                schema: "Products",
                table: "ExportJobs",
                columns: new[] { "RequestedBy", "QueuedOn" });

            migrationBuilder.CreateIndex(
                name: "IX_ExportJobs_Status_StartedOn",
                schema: "Products",
                table: "ExportJobs",
                columns: new[] { "Status", "StartedOn" });

            migrationBuilder.CreateIndex(
                name: "UX_ExportJobs_RequestedBy_Active",
                schema: "Products",
                table: "ExportJobs",
                column: "RequestedBy",
                unique: true,
                filter: "\"Status\" IN (0, 1)");

            migrationBuilder.CreateIndex(
                name: "IX_ImportJobs_FinishedOn",
                schema: "Products",
                table: "ImportJobs",
                column: "FinishedOn");

            migrationBuilder.CreateIndex(
                name: "IX_ImportJobs_RequestedBy_QueuedOn",
                schema: "Products",
                table: "ImportJobs",
                columns: new[] { "RequestedBy", "QueuedOn" });

            migrationBuilder.CreateIndex(
                name: "IX_ImportJobs_Status_StartedOn",
                schema: "Products",
                table: "ImportJobs",
                columns: new[] { "Status", "StartedOn" });

            migrationBuilder.CreateIndex(
                name: "UX_ImportJobs_RequestedBy_Active",
                schema: "Products",
                table: "ImportJobs",
                column: "RequestedBy",
                unique: true,
                filter: "\"Status\" IN (0, 1)");

            migrationBuilder.CreateIndex(
                name: "UX_ImportJobs_RequestedBy_ClientRequestId",
                schema: "Products",
                table: "ImportJobs",
                columns: new[] { "RequestedBy", "ClientRequestId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId_ObjectKey",
                schema: "Products",
                table: "ProductImages",
                columns: new[] { "ProductId", "ObjectKey" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportJobs",
                schema: "Products");

            migrationBuilder.DropTable(
                name: "ImportJobs",
                schema: "Products");

            migrationBuilder.DropTable(
                name: "ProductImages",
                schema: "Products");
        }
    }
}
