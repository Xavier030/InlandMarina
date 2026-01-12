using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InlandMarinaData.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Lease",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Lease",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Lease",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Lease",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Active", "EndDate", "StartDate" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Lease",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "Active", "EndDate", "StartDate" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Lease",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "Active", "EndDate", "StartDate" },
                values: new object[] { false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Lease");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Lease");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Lease");
        }
    }
}
