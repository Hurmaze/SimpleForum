using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class Refactored_Account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Email",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Comments",
                newName: "ThreadId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                newName: "IX_Comments_ThreadId");

            migrationBuilder.AlterColumn<string>(
                name: "Theme",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "AccountAuths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountAuths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountAuths_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "FK_Comments_Posts_ThreadId",
                table: "Comments",
                column: "ThreadId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_ThreadId",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "AccountAuths");

            migrationBuilder.RenameColumn(
                name: "ThreadId",
                table: "Comments",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ThreadId",
                table: "Comments",
                newName: "IX_Comments_PostId");

            migrationBuilder.AlterColumn<int>(
                name: "Theme",
                table: "Posts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Accounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Accounts",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Accounts",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
