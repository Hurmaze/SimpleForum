using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "Id", "ThemeName" },
                values: new object[,]
                {
                    { 1, "Books" },
                    { 2, "Elephants" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Nickname" },
                values: new object[,]
                {
                    { 1, "user1@gmail.com", "user1" },
                    { 2, "user2@gmail.com", "user2" },
                    { 3, "user3@gmail.com", "user3" },
                    { 4, "moderator1@gmail.com", "moderator1" },
                    { 5, "admin1@gmail.com", "admin1" }
                });

            migrationBuilder.InsertData(
                table: "Threads",
                columns: new[] { "Id", "AuthorId", "Content", "ThemeId", "TimeCreated", "Title" },
                values: new object[] { 1, 1, "Some text", 1, new DateTime(2022, 6, 22, 14, 52, 22, 53, DateTimeKind.Local).AddTicks(8570), "Super elephants" });

            migrationBuilder.InsertData(
                table: "Threads",
                columns: new[] { "Id", "AuthorId", "Content", "ThemeId", "TimeCreated", "Title" },
                values: new object[] { 2, 2, "My first book was...", 2, new DateTime(2022, 6, 22, 14, 52, 22, 53, DateTimeKind.Local).AddTicks(8574), "Man I love books" });

            migrationBuilder.InsertData(
                table: "ThreadPosts",
                columns: new[] { "Id", "AuthorId", "Content", "ThreadId", "TimeCreated" },
                values: new object[,]
                {
                    { 1, 2, "Man i love elephants!", 1, new DateTime(2022, 6, 22, 14, 52, 22, 53, DateTimeKind.Local).AddTicks(8586) },
                    { 2, 3, "My favourite elephant is...", 1, new DateTime(2022, 6, 22, 14, 52, 22, 53, DateTimeKind.Local).AddTicks(8589) },
                    { 3, 5, "Books are great you know.", 2, new DateTime(2022, 6, 22, 14, 52, 22, 53, DateTimeKind.Local).AddTicks(8591) },
                    { 4, 1, "Read recently about Segriy Zhadan... He is cool.", 2, new DateTime(2022, 6, 22, 14, 52, 22, 53, DateTimeKind.Local).AddTicks(8593) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
