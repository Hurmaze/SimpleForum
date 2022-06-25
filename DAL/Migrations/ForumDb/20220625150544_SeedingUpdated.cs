using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class SeedingUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "TimeCreated" },
                values: new object[] { "Man i love elephants!I recently learned that elephants drink up to 300 liters of water a day!", new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5645) });

            migrationBuilder.UpdateData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "TimeCreated" },
                values: new object[] { "My favourite elephant is Asian elephant", new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5650) });

            migrationBuilder.UpdateData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeCreated",
                value: new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5652));

            migrationBuilder.UpdateData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 4,
                column: "TimeCreated",
                value: new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5654));

            migrationBuilder.UpdateData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "TimeCreated" },
                values: new object[] { "Elephants are the largest existing land animals. Three living species are currently recognised: the African bush elephant, the African forest elephant, and the Asian elephant. They are an informal grouping within the subfamily Elephantinae of the order Proboscidea; extinct members include the mastodons.", new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5626) });

            migrationBuilder.UpdateData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "TimeCreated", "Title" },
                values: new object[] { "Let`s talk about Mykola Khvylovy and his novel 'I(Romance)' ", new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5631), "Mykola Khvylovy" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "TimeCreated" },
                values: new object[] { "Man i love elephants!", new DateTime(2022, 6, 22, 14, 52, 22, 53, DateTimeKind.Local).AddTicks(8586) });

            migrationBuilder.UpdateData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "TimeCreated" },
                values: new object[] { "My favourite elephant is...", new DateTime(2022, 6, 22, 14, 52, 22, 53, DateTimeKind.Local).AddTicks(8589) });

            migrationBuilder.UpdateData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeCreated",
                value: new DateTime(2022, 6, 22, 14, 52, 22, 53, DateTimeKind.Local).AddTicks(8591));

            migrationBuilder.UpdateData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 4,
                column: "TimeCreated",
                value: new DateTime(2022, 6, 22, 14, 52, 22, 53, DateTimeKind.Local).AddTicks(8593));

            migrationBuilder.UpdateData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Content", "TimeCreated" },
                values: new object[] { "Some text", new DateTime(2022, 6, 22, 14, 52, 22, 53, DateTimeKind.Local).AddTicks(8570) });

            migrationBuilder.UpdateData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "TimeCreated", "Title" },
                values: new object[] { "My first book was...", new DateTime(2022, 6, 22, 14, 52, 22, 53, DateTimeKind.Local).AddTicks(8574), "Man I love books" });
        }
    }
}
