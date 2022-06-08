using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class Renamed_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Accounts_AuthorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_ThreadId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Accounts_AuthorId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Threads");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "ThreadPosts");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_AuthorId",
                table: "Threads",
                newName: "IX_Threads_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ThreadId",
                table: "ThreadPosts",
                newName: "IX_ThreadPosts_ThreadId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AuthorId",
                table: "ThreadPosts",
                newName: "IX_ThreadPosts_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Threads",
                table: "Threads",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ThreadPosts",
                table: "ThreadPosts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadPosts_Accounts_AuthorId",
                table: "ThreadPosts",
                column: "AuthorId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadPosts_Threads_ThreadId",
                table: "ThreadPosts",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Accounts_AuthorId",
                table: "Threads",
                column: "AuthorId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadPosts_Accounts_AuthorId",
                table: "ThreadPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadPosts_Threads_ThreadId",
                table: "ThreadPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Accounts_AuthorId",
                table: "Threads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Threads",
                table: "Threads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ThreadPosts",
                table: "ThreadPosts");

            migrationBuilder.RenameTable(
                name: "Threads",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "ThreadPosts",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_AuthorId",
                table: "Posts",
                newName: "IX_Posts_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_ThreadPosts_ThreadId",
                table: "Comments",
                newName: "IX_Comments_ThreadId");

            migrationBuilder.RenameIndex(
                name: "IX_ThreadPosts_AuthorId",
                table: "Comments",
                newName: "IX_Comments_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Accounts_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_ThreadId",
                table: "Comments",
                column: "ThreadId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Accounts_AuthorId",
                table: "Posts",
                column: "AuthorId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
