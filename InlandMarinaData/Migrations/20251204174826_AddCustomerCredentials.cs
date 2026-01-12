using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InlandMarinaData.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerCredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Customer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Customer",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Password", "Username" },
                values: new object[] { "Password123", "johndoe" });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Password", "Username" },
                values: new object[] { "SecurePass456", "saraw" });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Password", "Username" },
                values: new object[] { "KenPassword789", "kenw" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Customer");
        }
    }
}
