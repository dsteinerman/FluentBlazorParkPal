using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FluentBlazorAuthTest.Migrations
{
    /// <inheritdoc />
    public partial class RoleAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11647c5e-bd37-4bc3-8738-8bdbae6d1883", "836131b8-a883-4c32-a39d-deb93bc567cd", "User", "USER" },
                    { "1a0776ca-397d-401c-a95a-e89a3f2fa185", "ed8f7cae-1463-4e8c-a524-5c4c5a3668cb", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11647c5e-bd37-4bc3-8738-8bdbae6d1883");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a0776ca-397d-401c-a95a-e89a3f2fa185");
        }
    }
}
