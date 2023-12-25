using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FluentBlazorAuthTest.Migrations
{
    /// <inheritdoc />
    public partial class addRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "18cba90c-5eda-46d8-9605-088cc124c87f", "c4a28642-836a-408b-a0d3-04661e7da1d9", "Admin", "ADMIN" },
                    { "3cde23b5-338f-4051-b9d1-52f24ed771fe", "52c7a2aa-1dc7-48dd-b267-6226fb8e87ab", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18cba90c-5eda-46d8-9605-088cc124c87f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cde23b5-338f-4051-b9d1-52f24ed771fe");
        }
    }
}
