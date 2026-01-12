using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InlandMarinaData.Migrations
{
    /// <inheritdoc />
    public partial class UsernamePasswordAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "City", "Password", "Phone", "Username" },
                values: new object[] { "Anytown", "password", "(555) 555-5555", "jdoe" });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "City", "FirstName", "LastName", "Password", "Phone", "Username" },
                values: new object[] { "Othertown", "Jane", "Smith", "password", "(555) 555-5556", "jsmith" });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "City", "FirstName", "LastName", "Password", "Phone", "Username" },
                values: new object[] { "Somewhere", "Robert", "Johnson", "password", "(555) 555-5557", "rjohnson" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "City", "Password", "Phone", "Username" },
                values: new object[] { "Phoenix", "Password123", "265-555-1212", "johndoe" });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "City", "FirstName", "LastName", "Password", "Phone", "Username" },
                values: new object[] { "Calgary", "Sara", "Williams", "SecurePass456", "403-555-9585", "saraw" });

            migrationBuilder.UpdateData(
                table: "Customer",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "City", "FirstName", "LastName", "Password", "Phone", "Username" },
                values: new object[] { "Kansas City", "Ken", "Wong", "KenPassword789", "802-555-3214", "kenw" });
        }
    }
}
