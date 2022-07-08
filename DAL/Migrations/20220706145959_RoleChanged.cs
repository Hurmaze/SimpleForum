using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class RoleChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BasicRole",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Credentials",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 22, 85, 217, 244, 121, 2, 104, 166, 233, 197, 69, 64, 51, 118, 251, 189, 166, 237, 224, 208, 83, 103, 19, 66, 108, 19, 48, 120, 151, 1, 69, 108, 108, 80, 238, 56, 83, 60, 207, 200, 111, 237, 208, 51, 223, 53, 66, 66, 33, 251, 135, 128, 92, 184, 54, 180, 11, 221, 22, 248, 3, 162, 99, 209 }, new byte[] { 17, 100, 6, 92, 115, 187, 109, 231, 120, 192, 65, 53, 186, 253, 10, 165, 136, 133, 163, 56, 146, 78, 28, 206, 225, 105, 170, 255, 103, 135, 141, 173, 22, 233, 248, 98, 181, 165, 116, 203, 48, 79, 144, 175, 81, 85, 6, 103, 149, 92, 170, 56, 110, 170, 243, 117, 189, 175, 250, 211, 242, 21, 101, 116, 18, 216, 38, 111, 98, 87, 58, 102, 117, 150, 213, 190, 166, 232, 231, 153, 227, 21, 173, 149, 128, 152, 87, 14, 142, 63, 25, 103, 28, 20, 152, 34, 99, 77, 168, 108, 21, 134, 2, 190, 169, 252, 145, 14, 237, 54, 29, 189, 107, 14, 22, 70, 168, 237, 58, 167, 150, 199, 216, 82, 111, 224, 78, 247 } });

            migrationBuilder.UpdateData(
                table: "Credentials",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 118, 16, 17, 88, 172, 106, 105, 100, 62, 180, 100, 37, 10, 8, 174, 106, 118, 71, 205, 109, 181, 24, 79, 238, 56, 18, 228, 252, 138, 150, 4, 67, 134, 175, 59, 153, 117, 120, 13, 91, 134, 242, 244, 227, 126, 117, 126, 241, 235, 59, 132, 110, 251, 186, 150, 89, 207, 145, 144, 47, 233, 10, 234, 97 }, new byte[] { 0, 26, 168, 154, 148, 147, 246, 99, 211, 214, 50, 26, 60, 255, 187, 98, 163, 25, 23, 197, 231, 111, 42, 148, 90, 13, 128, 184, 239, 157, 93, 186, 60, 134, 5, 75, 116, 120, 154, 52, 136, 181, 152, 59, 8, 133, 116, 152, 217, 220, 192, 46, 148, 94, 198, 169, 243, 225, 66, 29, 214, 239, 60, 186, 122, 207, 150, 250, 180, 111, 148, 174, 241, 17, 110, 48, 69, 194, 63, 48, 6, 41, 230, 220, 27, 21, 127, 243, 199, 238, 97, 117, 130, 48, 25, 2, 72, 159, 222, 142, 53, 166, 221, 193, 253, 122, 121, 207, 98, 253, 144, 100, 208, 87, 145, 212, 23, 117, 55, 108, 186, 239, 129, 74, 102, 77, 203, 251 } });

            migrationBuilder.UpdateData(
                table: "Credentials",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 56, 109, 146, 252, 87, 225, 128, 242, 105, 128, 9, 176, 144, 126, 3, 132, 218, 29, 105, 194, 11, 150, 39, 112, 182, 253, 253, 102, 37, 32, 74, 35, 202, 199, 68, 85, 28, 59, 44, 156, 148, 42, 123, 202, 196, 63, 31, 42, 164, 188, 122, 151, 140, 193, 37, 97, 99, 241, 33, 54, 215, 64, 238, 208 }, new byte[] { 104, 106, 117, 196, 209, 213, 168, 146, 54, 42, 180, 152, 62, 188, 116, 205, 166, 2, 22, 129, 72, 41, 55, 87, 197, 178, 152, 195, 162, 118, 76, 206, 107, 25, 81, 212, 63, 209, 70, 190, 46, 111, 94, 194, 159, 208, 18, 228, 141, 96, 78, 122, 230, 188, 244, 6, 98, 122, 118, 12, 209, 208, 27, 156, 117, 252, 10, 202, 201, 91, 209, 70, 128, 200, 50, 253, 24, 118, 36, 103, 110, 131, 181, 77, 33, 175, 7, 198, 184, 111, 55, 164, 226, 99, 196, 195, 243, 109, 5, 23, 60, 146, 6, 12, 44, 110, 220, 209, 204, 139, 133, 204, 208, 178, 92, 14, 248, 123, 107, 180, 111, 138, 231, 81, 180, 169, 8, 147 } });

            migrationBuilder.UpdateData(
                table: "Credentials",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 240, 171, 251, 15, 91, 59, 47, 18, 87, 202, 161, 32, 110, 194, 148, 72, 248, 178, 86, 216, 141, 139, 241, 69, 111, 84, 46, 13, 72, 5, 211, 147, 235, 36, 165, 88, 93, 41, 124, 160, 111, 124, 3, 50, 248, 75, 42, 247, 180, 161, 237, 131, 178, 12, 212, 197, 24, 76, 4, 201, 135, 125, 136, 105 }, new byte[] { 138, 82, 74, 202, 110, 65, 60, 245, 62, 218, 169, 234, 114, 96, 204, 39, 174, 232, 75, 131, 39, 234, 111, 104, 86, 168, 236, 243, 201, 55, 25, 134, 54, 154, 66, 12, 126, 26, 103, 172, 166, 255, 133, 48, 231, 87, 178, 12, 34, 243, 33, 142, 150, 190, 91, 5, 157, 213, 52, 165, 231, 240, 171, 255, 173, 137, 152, 121, 97, 224, 254, 236, 84, 116, 1, 157, 76, 202, 82, 238, 191, 113, 170, 147, 157, 7, 9, 199, 126, 67, 55, 161, 169, 120, 217, 183, 36, 111, 51, 55, 119, 14, 116, 0, 66, 61, 81, 19, 18, 243, 245, 184, 44, 237, 215, 93, 68, 46, 133, 187, 82, 143, 123, 107, 48, 137, 134, 74 } });

            migrationBuilder.UpdateData(
                table: "Credentials",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 219, 117, 26, 251, 206, 75, 202, 135, 248, 10, 143, 21, 98, 87, 15, 26, 25, 251, 96, 131, 38, 218, 131, 51, 194, 9, 87, 24, 8, 65, 211, 202, 56, 82, 1, 228, 230, 25, 124, 107, 154, 132, 63, 30, 40, 33, 212, 80, 204, 28, 16, 89, 6, 100, 61, 39, 83, 235, 61, 109, 135, 206, 178, 232 }, new byte[] { 248, 201, 33, 245, 173, 31, 122, 232, 21, 39, 34, 110, 200, 46, 239, 108, 232, 33, 139, 243, 167, 125, 116, 136, 76, 205, 93, 236, 198, 46, 28, 184, 134, 195, 4, 131, 129, 162, 227, 152, 69, 36, 15, 21, 37, 158, 137, 89, 74, 43, 245, 25, 152, 186, 21, 0, 211, 183, 199, 90, 173, 149, 148, 208, 217, 167, 207, 16, 94, 195, 234, 70, 94, 182, 195, 26, 127, 200, 151, 116, 127, 238, 218, 68, 193, 134, 173, 55, 44, 89, 118, 220, 70, 226, 172, 98, 173, 149, 153, 5, 153, 144, 212, 198, 163, 111, 60, 79, 30, 13, 2, 74, 237, 227, 70, 193, 23, 101, 108, 11, 9, 114, 73, 113, 52, 232, 13, 155 } });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeCreated",
                value: new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9247));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeCreated",
                value: new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9250));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeCreated",
                value: new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9252));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "TimeCreated",
                value: new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9254));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "BasicRole",
                value: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "BasicRole",
                value: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "BasicRole",
                value: true);

            migrationBuilder.UpdateData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeCreated",
                value: new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9231));

            migrationBuilder.UpdateData(
                table: "Threads",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeCreated",
                value: new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9235));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "RegistrationTime",
                value: new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9080));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "RegistrationTime",
                value: new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9113));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "RegistrationTime",
                value: new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9116));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "RegistrationTime",
                value: new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9117));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "RegistrationTime",
                value: new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9119));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasicRole",
                table: "Roles");

            migrationBuilder.UpdateData(
                table: "Credentials",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 82, 211, 234, 79, 195, 143, 253, 183, 24, 24, 132, 12, 14, 220, 206, 177, 16, 248, 9, 221, 130, 152, 21, 238, 57, 222, 119, 253, 2, 38, 97, 13, 152, 96, 53, 239, 65, 192, 16, 252, 55, 126, 151, 61, 223, 251, 152, 205, 69, 232, 11, 0, 219, 68, 221, 216, 225, 134, 216, 66, 25, 203, 23, 71 }, new byte[] { 88, 83, 161, 164, 78, 87, 146, 71, 0, 126, 165, 242, 144, 84, 58, 216, 206, 179, 28, 128, 223, 170, 10, 135, 112, 42, 11, 71, 41, 198, 77, 173, 174, 17, 65, 142, 218, 29, 171, 101, 138, 220, 134, 1, 146, 108, 140, 104, 19, 119, 200, 48, 33, 98, 23, 172, 64, 101, 121, 229, 94, 144, 244, 249, 33, 214, 156, 131, 222, 155, 108, 103, 36, 95, 220, 238, 77, 77, 15, 29, 211, 6, 4, 122, 38, 145, 135, 69, 26, 222, 223, 166, 200, 114, 224, 19, 187, 156, 175, 225, 39, 170, 49, 53, 44, 89, 96, 74, 215, 160, 167, 232, 2, 39, 45, 218, 49, 124, 11, 153, 44, 139, 35, 222, 114, 105, 233, 250 } });

            migrationBuilder.UpdateData(
                table: "Credentials",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 108, 67, 129, 71, 5, 88, 244, 52, 177, 184, 142, 222, 42, 99, 89, 221, 1, 240, 253, 27, 177, 10, 14, 128, 151, 115, 169, 111, 55, 131, 113, 28, 213, 206, 230, 49, 76, 110, 54, 32, 126, 153, 44, 189, 5, 0, 230, 50, 52, 149, 20, 58, 157, 8, 98, 4, 117, 100, 74, 145, 188, 225, 44, 180 }, new byte[] { 158, 214, 243, 190, 224, 15, 210, 163, 189, 112, 172, 113, 240, 202, 153, 99, 1, 107, 230, 231, 46, 163, 236, 56, 124, 10, 200, 238, 202, 0, 74, 22, 220, 111, 202, 109, 67, 92, 189, 213, 135, 183, 221, 209, 67, 246, 231, 209, 59, 182, 55, 19, 212, 179, 143, 156, 205, 204, 151, 150, 75, 4, 71, 188, 20, 69, 162, 23, 166, 118, 138, 158, 11, 113, 223, 247, 225, 101, 245, 117, 184, 42, 243, 133, 218, 109, 57, 113, 20, 142, 21, 114, 134, 171, 75, 89, 83, 139, 13, 75, 163, 230, 68, 233, 144, 208, 89, 120, 166, 86, 8, 202, 201, 65, 19, 49, 130, 243, 73, 35, 50, 45, 211, 54, 14, 2, 63, 1 } });

            migrationBuilder.UpdateData(
                table: "Credentials",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 25, 253, 52, 190, 123, 194, 234, 204, 202, 42, 185, 26, 224, 97, 165, 5, 255, 96, 189, 253, 146, 130, 166, 68, 157, 81, 237, 116, 46, 74, 105, 149, 198, 208, 110, 89, 180, 228, 175, 170, 228, 83, 17, 228, 42, 61, 218, 209, 192, 171, 217, 13, 49, 77, 16, 110, 185, 25, 122, 169, 126, 231, 72, 36 }, new byte[] { 109, 200, 207, 122, 243, 65, 207, 128, 177, 205, 220, 152, 50, 20, 89, 60, 76, 67, 151, 148, 125, 57, 140, 22, 47, 204, 254, 228, 16, 214, 160, 70, 27, 12, 102, 13, 196, 131, 117, 77, 48, 97, 88, 135, 244, 83, 47, 50, 177, 200, 52, 22, 103, 81, 216, 0, 222, 91, 229, 2, 205, 241, 216, 121, 244, 131, 123, 118, 220, 45, 211, 74, 187, 189, 37, 254, 79, 255, 213, 194, 102, 66, 210, 238, 202, 247, 78, 253, 21, 119, 149, 96, 152, 50, 227, 47, 12, 29, 112, 29, 131, 53, 44, 252, 19, 38, 64, 2, 46, 68, 151, 186, 173, 71, 218, 161, 137, 12, 228, 43, 101, 222, 241, 50, 125, 59, 132, 241 } });

            migrationBuilder.UpdateData(
                table: "Credentials",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 66, 104, 115, 32, 190, 172, 184, 192, 20, 64, 140, 32, 144, 127, 25, 215, 189, 204, 2, 130, 236, 204, 37, 52, 34, 114, 179, 212, 175, 223, 160, 171, 99, 124, 202, 84, 62, 155, 203, 124, 189, 216, 21, 38, 227, 241, 132, 148, 165, 0, 86, 133, 40, 52, 206, 138, 169, 99, 250, 130, 255, 21, 20, 115 }, new byte[] { 95, 66, 24, 100, 107, 200, 76, 43, 212, 221, 54, 6, 29, 29, 251, 123, 23, 24, 2, 25, 207, 177, 79, 23, 183, 181, 180, 8, 165, 79, 56, 75, 242, 133, 106, 77, 47, 102, 72, 57, 212, 86, 105, 208, 53, 32, 208, 117, 191, 93, 205, 118, 165, 107, 74, 85, 149, 110, 150, 181, 142, 69, 213, 10, 149, 86, 197, 150, 54, 81, 58, 161, 112, 172, 146, 114, 246, 110, 75, 234, 99, 191, 95, 10, 14, 249, 183, 201, 7, 206, 208, 105, 225, 11, 174, 227, 247, 13, 229, 121, 99, 76, 92, 22, 153, 51, 17, 61, 184, 128, 255, 143, 195, 141, 148, 193, 150, 126, 149, 29, 71, 234, 68, 169, 28, 247, 197, 168 } });

            migrationBuilder.UpdateData(
                table: "Credentials",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 33, 103, 168, 195, 193, 227, 104, 217, 148, 250, 194, 117, 164, 192, 161, 209, 40, 162, 250, 155, 189, 249, 35, 214, 29, 144, 13, 71, 159, 216, 249, 245, 40, 34, 140, 81, 198, 40, 147, 222, 101, 165, 173, 1, 151, 195, 255, 17, 191, 31, 189, 68, 82, 242, 55, 17, 246, 110, 173, 199, 12, 133, 15, 199 }, new byte[] { 179, 215, 240, 224, 166, 244, 163, 56, 11, 49, 5, 213, 5, 90, 243, 2, 122, 152, 145, 219, 141, 241, 104, 190, 57, 38, 60, 57, 36, 27, 230, 91, 167, 52, 192, 14, 160, 228, 57, 34, 21, 186, 13, 84, 7, 249, 169, 106, 3, 91, 47, 211, 141, 12, 142, 241, 249, 17, 102, 26, 58, 197, 28, 36, 162, 191, 38, 235, 163, 236, 155, 188, 128, 89, 58, 9, 112, 203, 121, 245, 90, 243, 247, 101, 156, 206, 90, 69, 226, 170, 209, 49, 94, 155, 3, 152, 215, 205, 80, 184, 174, 124, 176, 86, 250, 72, 89, 214, 144, 53, 204, 135, 190, 209, 220, 100, 193, 210, 29, 29, 101, 99, 13, 170, 104, 38, 139, 49 } });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeCreated",
                value: new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8514));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeCreated",
                value: new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8518));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "TimeCreated",
                value: new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8521));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 4,
                column: "TimeCreated",
                value: new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8524));

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
                column: "RegistrationTime",
                value: new DateTime(2022, 7, 4, 13, 20, 26, 715, DateTimeKind.Local).AddTicks(8303));

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
        }
    }
}
