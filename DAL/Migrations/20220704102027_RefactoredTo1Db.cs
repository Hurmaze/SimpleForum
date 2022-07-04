using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class RefactoredTo1Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThreadPosts");

            migrationBuilder.AddColumn<int>(
                name: "CredentialsId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationTime",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    ThreadId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Threads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credentials_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Credentials_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Content", "ThreadId", "TimeCreated" },
                values: new object[,]
                {
                    { 1, 2, "Man i love elephants!I recently learned that elephants drink up to 300 liters of water a day!", 1, new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8514) },
                    { 2, 3, "My favourite elephant is Asian elephant", 1, new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8518) },
                    { 3, 5, "Books are great you know.", 2, new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8521) },
                    { 4, 1, "Read recently about Segriy Zhadan... He is cool.", 2, new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8524) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "user" },
                    { 2, "moderator" },
                    { 3, "admin" }
                });

            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "Id", "ThemeName" },
                values: new object[] { 3, "Other" });

            migrationBuilder.UpdateData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeCreated",
                value: new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8488));

            migrationBuilder.UpdateData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeCreated",
                value: new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8494));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationTime",
                value: new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8258));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Nickname", "RegistrationTime" },
                values: new object[] { null, new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8303) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegistrationTime",
                value: new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8307));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "RegistrationTime",
                value: new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8310));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "RegistrationTime",
                value: new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8312));

            migrationBuilder.InsertData(
                table: "Credentials",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, new byte[] { 82, 211, 234, 79, 195, 143, 253, 183, 24, 24, 132, 12, 14, 220, 206, 177, 16, 248, 9, 221, 130, 152, 21, 238, 57, 222, 119, 253, 2, 38, 97, 13, 152, 96, 53, 239, 65, 192, 16, 252, 55, 126, 151, 61, 223, 251, 152, 205, 69, 232, 11, 0, 219, 68, 221, 216, 225, 134, 216, 66, 25, 203, 23, 71 }, new byte[] { 88, 83, 161, 164, 78, 87, 146, 71, 0, 126, 165, 242, 144, 84, 58, 216, 206, 179, 28, 128, 223, 170, 10, 135, 112, 42, 11, 71, 41, 198, 77, 173, 174, 17, 65, 142, 218, 29, 171, 101, 138, 220, 134, 1, 146, 108, 140, 104, 19, 119, 200, 48, 33, 98, 23, 172, 64, 101, 121, 229, 94, 144, 244, 249, 33, 214, 156, 131, 222, 155, 108, 103, 36, 95, 220, 238, 77, 77, 15, 29, 211, 6, 4, 122, 38, 145, 135, 69, 26, 222, 223, 166, 200, 114, 224, 19, 187, 156, 175, 225, 39, 170, 49, 53, 44, 89, 96, 74, 215, 160, 167, 232, 2, 39, 45, 218, 49, 124, 11, 153, 44, 139, 35, 222, 114, 105, 233, 250 }, 1, 1 },
                    { 2, new byte[] { 108, 67, 129, 71, 5, 88, 244, 52, 177, 184, 142, 222, 42, 99, 89, 221, 1, 240, 253, 27, 177, 10, 14, 128, 151, 115, 169, 111, 55, 131, 113, 28, 213, 206, 230, 49, 76, 110, 54, 32, 126, 153, 44, 189, 5, 0, 230, 50, 52, 149, 20, 58, 157, 8, 98, 4, 117, 100, 74, 145, 188, 225, 44, 180 }, new byte[] { 158, 214, 243, 190, 224, 15, 210, 163, 189, 112, 172, 113, 240, 202, 153, 99, 1, 107, 230, 231, 46, 163, 236, 56, 124, 10, 200, 238, 202, 0, 74, 22, 220, 111, 202, 109, 67, 92, 189, 213, 135, 183, 221, 209, 67, 246, 231, 209, 59, 182, 55, 19, 212, 179, 143, 156, 205, 204, 151, 150, 75, 4, 71, 188, 20, 69, 162, 23, 166, 118, 138, 158, 11, 113, 223, 247, 225, 101, 245, 117, 184, 42, 243, 133, 218, 109, 57, 113, 20, 142, 21, 114, 134, 171, 75, 89, 83, 139, 13, 75, 163, 230, 68, 233, 144, 208, 89, 120, 166, 86, 8, 202, 201, 65, 19, 49, 130, 243, 73, 35, 50, 45, 211, 54, 14, 2, 63, 1 }, 1, 2 },
                    { 3, new byte[] { 25, 253, 52, 190, 123, 194, 234, 204, 202, 42, 185, 26, 224, 97, 165, 5, 255, 96, 189, 253, 146, 130, 166, 68, 157, 81, 237, 116, 46, 74, 105, 149, 198, 208, 110, 89, 180, 228, 175, 170, 228, 83, 17, 228, 42, 61, 218, 209, 192, 171, 217, 13, 49, 77, 16, 110, 185, 25, 122, 169, 126, 231, 72, 36 }, new byte[] { 109, 200, 207, 122, 243, 65, 207, 128, 177, 205, 220, 152, 50, 20, 89, 60, 76, 67, 151, 148, 125, 57, 140, 22, 47, 204, 254, 228, 16, 214, 160, 70, 27, 12, 102, 13, 196, 131, 117, 77, 48, 97, 88, 135, 244, 83, 47, 50, 177, 200, 52, 22, 103, 81, 216, 0, 222, 91, 229, 2, 205, 241, 216, 121, 244, 131, 123, 118, 220, 45, 211, 74, 187, 189, 37, 254, 79, 255, 213, 194, 102, 66, 210, 238, 202, 247, 78, 253, 21, 119, 149, 96, 152, 50, 227, 47, 12, 29, 112, 29, 131, 53, 44, 252, 19, 38, 64, 2, 46, 68, 151, 186, 173, 71, 218, 161, 137, 12, 228, 43, 101, 222, 241, 50, 125, 59, 132, 241 }, 1, 3 },
                    { 4, new byte[] { 66, 104, 115, 32, 190, 172, 184, 192, 20, 64, 140, 32, 144, 127, 25, 215, 189, 204, 2, 130, 236, 204, 37, 52, 34, 114, 179, 212, 175, 223, 160, 171, 99, 124, 202, 84, 62, 155, 203, 124, 189, 216, 21, 38, 227, 241, 132, 148, 165, 0, 86, 133, 40, 52, 206, 138, 169, 99, 250, 130, 255, 21, 20, 115 }, new byte[] { 95, 66, 24, 100, 107, 200, 76, 43, 212, 221, 54, 6, 29, 29, 251, 123, 23, 24, 2, 25, 207, 177, 79, 23, 183, 181, 180, 8, 165, 79, 56, 75, 242, 133, 106, 77, 47, 102, 72, 57, 212, 86, 105, 208, 53, 32, 208, 117, 191, 93, 205, 118, 165, 107, 74, 85, 149, 110, 150, 181, 142, 69, 213, 10, 149, 86, 197, 150, 54, 81, 58, 161, 112, 172, 146, 114, 246, 110, 75, 234, 99, 191, 95, 10, 14, 249, 183, 201, 7, 206, 208, 105, 225, 11, 174, 227, 247, 13, 229, 121, 99, 76, 92, 22, 153, 51, 17, 61, 184, 128, 255, 143, 195, 141, 148, 193, 150, 126, 149, 29, 71, 234, 68, 169, 28, 247, 197, 168 }, 2, 4 },
                    { 5, new byte[] { 33, 103, 168, 195, 193, 227, 104, 217, 148, 250, 194, 117, 164, 192, 161, 209, 40, 162, 250, 155, 189, 249, 35, 214, 29, 144, 13, 71, 159, 216, 249, 245, 40, 34, 140, 81, 198, 40, 147, 222, 101, 165, 173, 1, 151, 195, 255, 17, 191, 31, 189, 68, 82, 242, 55, 17, 246, 110, 173, 199, 12, 133, 15, 199 }, new byte[] { 179, 215, 240, 224, 166, 244, 163, 56, 11, 49, 5, 213, 5, 90, 243, 2, 122, 152, 145, 219, 141, 241, 104, 190, 57, 38, 60, 57, 36, 27, 230, 91, 167, 52, 192, 14, 160, 228, 57, 34, 21, 186, 13, 84, 7, 249, 169, 106, 3, 91, 47, 211, 141, 12, 142, 241, 249, 17, 102, 26, 58, 197, 28, 36, 162, 191, 38, 235, 163, 236, 155, 188, 128, 89, 58, 9, 112, 203, 121, 245, 90, 243, 247, 101, 156, 206, 90, 69, 226, 170, 209, 49, 94, 155, 3, 152, 215, 205, 80, 184, 174, 124, 176, 86, 250, 72, 89, 214, 144, 53, 204, 135, 190, 209, 220, 100, 193, 210, 29, 29, 101, 99, 13, 170, 104, 38, 139, 49 }, 3, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_RoleId",
                table: "Credentials",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_UserId",
                table: "Credentials",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorId",
                table: "Posts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ThreadId",
                table: "Posts",
                column: "ThreadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DeleteData(
                table: "Themes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "CredentialsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegistrationTime",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "ThreadPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    ThreadId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadPosts_Threads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThreadPosts_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "ThreadPosts",
                columns: new[] { "Id", "AuthorId", "Content", "ThreadId", "TimeCreated" },
                values: new object[,]
                {
                    { 1, 2, "Man i love elephants!I recently learned that elephants drink up to 300 liters of water a day!", 1, new DateTime(2022, 6, 29, 12, 59, 0, 506, DateTimeKind.Local).AddTicks(7350) },
                    { 2, 3, "My favourite elephant is Asian elephant", 1, new DateTime(2022, 6, 29, 12, 59, 0, 506, DateTimeKind.Local).AddTicks(7354) },
                    { 3, 5, "Books are great you know.", 2, new DateTime(2022, 6, 29, 12, 59, 0, 506, DateTimeKind.Local).AddTicks(7356) },
                    { 4, 1, "Read recently about Segriy Zhadan... He is cool.", 2, new DateTime(2022, 6, 29, 12, 59, 0, 506, DateTimeKind.Local).AddTicks(7358) }
                });

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nickname",
                value: "user2");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadPosts_AuthorId",
                table: "ThreadPosts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadPosts_ThreadId",
                table: "ThreadPosts",
                column: "ThreadId");
        }
    }
}
