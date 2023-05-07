using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class standarddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "DateEnrolled", "Email", "Password", "Role" },
                values: new object[] { new Guid("9efe7028-c186-46d1-a623-7b66038d70b8"), new DateTime(2023, 4, 26, 14, 27, 9, 401, DateTimeKind.Local).AddTicks(1828), "string", "$2a$11$708iVVj6xTVtU6LoUdl95.7CAyoD0mz/w243ox9S6cIqR2iWX63Ky", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("9efe7028-c186-46d1-a623-7b66038d70b8"));
        }
    }
}
