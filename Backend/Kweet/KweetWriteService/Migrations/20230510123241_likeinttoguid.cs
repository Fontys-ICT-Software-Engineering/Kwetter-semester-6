using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Kweet.Migrations
{
    /// <inheritdoc />
    public partial class likeinttoguid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Likes",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.InsertData(
                table: "Kweets",
                columns: new[] { "Id", "Date", "IsEdited", "Message", "User" },
                values: new object[,]
                {
                    { new Guid("09a6898f-f1b9-4d2c-9e68-c555ae1bd8ed"), new DateTime(2023, 5, 10, 14, 32, 41, 696, DateTimeKind.Local).AddTicks(4438), false, "testMessage 6", "User2" },
                    { new Guid("4d18d8b7-f3a6-4d78-80e0-768af0bbc19a"), new DateTime(2023, 5, 10, 14, 32, 41, 696, DateTimeKind.Local).AddTicks(4423), true, "testMessage 5", "User2" },
                    { new Guid("6d237924-a3fe-454d-9d61-afa9359205c2"), new DateTime(2023, 5, 10, 14, 32, 41, 696, DateTimeKind.Local).AddTicks(4348), true, "testMessage 2", "User1" },
                    { new Guid("9d382436-f61d-4f8f-aca4-265c3b058488"), new DateTime(2023, 5, 10, 14, 32, 41, 696, DateTimeKind.Local).AddTicks(4269), false, "testMessage 1", "User1" },
                    { new Guid("9e7e04c6-692d-4f25-9ccc-6541acb9add8"), new DateTime(2023, 5, 10, 14, 32, 41, 696, DateTimeKind.Local).AddTicks(4373), false, "testMessage 3", "User1" },
                    { new Guid("daf79d49-6e0d-44fc-942f-551b29025fba"), new DateTime(2023, 5, 10, 14, 32, 41, 696, DateTimeKind.Local).AddTicks(4385), false, "testMessage 4", "User1" }
                });

            migrationBuilder.InsertData(
                table: "Likes",
                columns: new[] { "Id", "KweetID", "UserID" },
                values: new object[,]
                {
                    { new Guid("b6f496f5-e8b0-4c9a-b3f8-a6cc2552f9c2"), "9d382436-f61d-4f8f-aca4-265c3b058488", "User1" },
                    { new Guid("bbd6fcd2-6022-46e6-9614-b173b0857865"), "9d382436-f61d-4f8f-aca4-265c3b058488", "User2" },
                    { new Guid("c905083a-0328-4f27-8992-7810dc15a28b"), "9d382436-f61d-4f8f-aca4-265c3b058488", "User1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Kweets",
                keyColumn: "Id",
                keyValue: new Guid("09a6898f-f1b9-4d2c-9e68-c555ae1bd8ed"));

            migrationBuilder.DeleteData(
                table: "Kweets",
                keyColumn: "Id",
                keyValue: new Guid("4d18d8b7-f3a6-4d78-80e0-768af0bbc19a"));

            migrationBuilder.DeleteData(
                table: "Kweets",
                keyColumn: "Id",
                keyValue: new Guid("6d237924-a3fe-454d-9d61-afa9359205c2"));

            migrationBuilder.DeleteData(
                table: "Kweets",
                keyColumn: "Id",
                keyValue: new Guid("9d382436-f61d-4f8f-aca4-265c3b058488"));

            migrationBuilder.DeleteData(
                table: "Kweets",
                keyColumn: "Id",
                keyValue: new Guid("9e7e04c6-692d-4f25-9ccc-6541acb9add8"));

            migrationBuilder.DeleteData(
                table: "Kweets",
                keyColumn: "Id",
                keyValue: new Guid("daf79d49-6e0d-44fc-942f-551b29025fba"));

            migrationBuilder.DeleteData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: new Guid("b6f496f5-e8b0-4c9a-b3f8-a6cc2552f9c2"));

            migrationBuilder.DeleteData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: new Guid("bbd6fcd2-6022-46e6-9614-b173b0857865"));

            migrationBuilder.DeleteData(
                table: "Likes",
                keyColumn: "Id",
                keyValue: new Guid("c905083a-0328-4f27-8992-7810dc15a28b"));

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Likes",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

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
    }
}
