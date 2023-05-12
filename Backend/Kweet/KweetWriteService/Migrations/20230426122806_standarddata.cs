using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kweet.Migrations
{
    /// <inheritdoc />
    public partial class standarddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Kweets",
                columns: new[] { "Id", "Date", "IsEdited", "Message", "User" },
                values: new object[,]
                {
                    { new Guid("0871edc8-1916-4ce6-9ed9-15c1fc38bb65"), new DateTime(2023, 4, 26, 14, 28, 6, 377, DateTimeKind.Local).AddTicks(1610), false, "testMessage 1", "User1" },
                    { new Guid("48ebef61-515d-4891-9aa3-82ff2d53bbdb"), new DateTime(2023, 4, 26, 14, 28, 6, 377, DateTimeKind.Local).AddTicks(1726), false, "testMessage 6", "User2" },
                    { new Guid("9c555efc-1721-4844-a8cc-24043dc97651"), new DateTime(2023, 4, 26, 14, 28, 6, 377, DateTimeKind.Local).AddTicks(1702), false, "testMessage 4", "User1" },
                    { new Guid("9fcb1430-a7ad-463e-9bd3-fa000656176e"), new DateTime(2023, 4, 26, 14, 28, 6, 377, DateTimeKind.Local).AddTicks(1713), true, "testMessage 5", "User2" },
                    { new Guid("ddd51194-7632-4d3b-9ab0-c9609ba5d73b"), new DateTime(2023, 4, 26, 14, 28, 6, 377, DateTimeKind.Local).AddTicks(1691), false, "testMessage 3", "User1" },
                    { new Guid("fb8afad9-dd53-4e02-8fe7-53eadce5992c"), new DateTime(2023, 4, 26, 14, 28, 6, 377, DateTimeKind.Local).AddTicks(1679), true, "testMessage 2", "User1" }
                });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "Id", "KweetID", "UserID" },
                values: new object[,]
                {
                    { 1, "0871edc8-1916-4ce6-9ed9-15c1fc38bb65", "User2" },
                    { 2, "0871edc8-1916-4ce6-9ed9-15c1fc38bb65", "User1" },
                    { 3, "0871edc8-1916-4ce6-9ed9-15c1fc38bb65", "User1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Kweets",
                keyColumn: "Id",
                keyValue: new Guid("0871edc8-1916-4ce6-9ed9-15c1fc38bb65"));

            migrationBuilder.DeleteData(
                table: "Kweets",
                keyColumn: "Id",
                keyValue: new Guid("48ebef61-515d-4891-9aa3-82ff2d53bbdb"));

            migrationBuilder.DeleteData(
                table: "Kweets",
                keyColumn: "Id",
                keyValue: new Guid("9c555efc-1721-4844-a8cc-24043dc97651"));

            migrationBuilder.DeleteData(
                table: "Kweets",
                keyColumn: "Id",
                keyValue: new Guid("9fcb1430-a7ad-463e-9bd3-fa000656176e"));

            migrationBuilder.DeleteData(
                table: "Kweets",
                keyColumn: "Id",
                keyValue: new Guid("ddd51194-7632-4d3b-9ab0-c9609ba5d73b"));

            migrationBuilder.DeleteData(
                table: "Kweets",
                keyColumn: "Id",
                keyValue: new Guid("fb8afad9-dd53-4e02-8fe7-53eadce5992c"));

            migrationBuilder.DeleteData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
