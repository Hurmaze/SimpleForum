using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class RefactoredAccountInto2Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountAuths_Accounts_AccountId",
                table: "AccountAuths");

            migrationBuilder.DropIndex(
                name: "IX_AccountAuths_AccountId",
                table: "AccountAuths");

            migrationBuilder.DropIndex(
                name: "IX_AccountAuths_Email",
                table: "AccountAuths");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "Threads",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "AccountAuths",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "AccountAuths",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AccountAuths",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "AccountAuths",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThemeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Threads_ThemeId",
                table: "Threads",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccountAuths_RoleId",
                table: "AccountAuths",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountAuths_Role_RoleId",
                table: "AccountAuths",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Themes_ThemeId",
                table: "Threads",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountAuths_Role_RoleId",
                table: "AccountAuths");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Themes_ThemeId",
                table: "Threads");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Themes");

            migrationBuilder.DropIndex(
                name: "IX_Threads_ThemeId",
                table: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Email",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_AccountAuths_RoleId",
                table: "AccountAuths");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AccountAuths");

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Threads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "AccountAuths",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "AccountAuths",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AccountAuths",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountAuths_AccountId",
                table: "AccountAuths",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountAuths_Email",
                table: "AccountAuths",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountAuths_Accounts_AccountId",
                table: "AccountAuths",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
