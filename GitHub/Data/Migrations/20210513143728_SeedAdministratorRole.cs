using Microsoft.EntityFrameworkCore.Migrations;

namespace GitHub.Data.Migrations
{
    public partial class SeedAdministratorRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2dd30fd0-001c-4638-b90c-d955dcd18ced", "2dd30fd0-001c-4638-b90c-d955dcd18ced", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2dd30fd0-001c-4638-b90c-d955dcd18ced");
        }
    }
}
