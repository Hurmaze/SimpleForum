using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class TableConfiguringImproved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadPosts_Users_AuthorId",
                table: "ThreadPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Themes_ThemeId",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Users_AuthorId",
                table: "Threads");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Threads",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "Threads",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Threads",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "ThreadPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "ThreadPosts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeCreated",
                value: new DateTime(2022, 6, 29, 12, 59, 0, 506, DateTimeKind.Local).AddTicks(7350));

            migrationBuilder.UpdateData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeCreated",
                value: new DateTime(2022, 6, 29, 12, 59, 0, 506, DateTimeKind.Local).AddTicks(7354));

            migrationBuilder.UpdateData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeCreated",
                value: new DateTime(2022, 6, 29, 12, 59, 0, 506, DateTimeKind.Local).AddTicks(7356));

            migrationBuilder.UpdateData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 4,
                column: "TimeCreated",
                value: new DateTime(2022, 6, 29, 12, 59, 0, 506, DateTimeKind.Local).AddTicks(7358));

            migrationBuilder.UpdateData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeCreated",
                value: new DateTime(2022, 6, 29, 12, 59, 0, 506, DateTimeKind.Local).AddTicks(7291));

            migrationBuilder.UpdateData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeCreated",
                value: new DateTime(2022, 6, 29, 12, 59, 0, 506, DateTimeKind.Local).AddTicks(7299));

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadPosts_Users_AuthorId",
                table: "ThreadPosts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Themes_ThemeId",
                table: "Threads",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Users_AuthorId",
                table: "Threads",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadPosts_Users_AuthorId",
                table: "ThreadPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Themes_ThemeId",
                table: "Threads");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Users_AuthorId",
                table: "Threads");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Threads",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "Threads",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Threads",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "ThreadPosts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "ThreadPosts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeCreated",
                value: new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5645));

            migrationBuilder.UpdateData(
                table: "ThreadPosts",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeCreated",
                value: new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5650));

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
                column: "TimeCreated",
                value: new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5626));

            migrationBuilder.UpdateData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeCreated",
                value: new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5631));

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadPosts_Users_AuthorId",
                table: "ThreadPosts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Themes_ThemeId",
                table: "Threads",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Users_AuthorId",
                table: "Threads",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
