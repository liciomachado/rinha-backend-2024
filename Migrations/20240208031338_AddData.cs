using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RinhaBackend2024.API.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "realized",
                schema: "public",
                table: "transaction",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                schema: "public",
                table: "clients",
                columns: new[] { "id", "balance", "limit" },
                values: new object[,]
                {
                    { 1L, 0L, 100000L },
                    { 2L, 0L, 80000L },
                    { 3L, 0L, 1000000L },
                    { 4L, 0L, 10000000L },
                    { 5L, 0L, 500000L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "clients",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "clients",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "clients",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "clients",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "public",
                table: "clients",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "realized",
                schema: "public",
                table: "transaction",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
