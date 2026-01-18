using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentManagementWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class insertingdefaultvalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Age", "CreatedDate", "Email", "Name" },
                values: new object[,]
                {
                    { 1, 21, new DateTime(2026, 1, 18, 14, 33, 11, 601, DateTimeKind.Local).AddTicks(1368), "jodnd@gmail.com", "John Doe" },
                    { 2, 22, new DateTime(2026, 1, 18, 14, 33, 11, 601, DateTimeKind.Local).AddTicks(1380), "Janes@gmail.com", "Jane Smith" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
