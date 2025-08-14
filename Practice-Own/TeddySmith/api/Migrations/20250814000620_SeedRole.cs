using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "109b3734-f313-4f4e-bf28-93fb48de7fa2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32d79ecb-4672-410a-b7ba-138775e6ff8e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9da2898f-e045-436d-b71f-16993138249b", null, "Admin", "ADMIN" },
                    { "f0ca0308-5b01-423f-bd0c-d1835f9facef", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9da2898f-e045-436d-b71f-16993138249b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0ca0308-5b01-423f-bd0c-d1835f9facef");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "109b3734-f313-4f4e-bf28-93fb48de7fa2", null, "Admin", "ADMIN" },
                    { "32d79ecb-4672-410a-b7ba-138775e6ff8e", null, "User", "USER" }
                });
        }
    }
}
