﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations.AuthenticationDb
{
    public partial class SeedingUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 41, 1, 202, 217, 199, 20, 212, 242, 221, 86, 196, 176, 142, 100, 199, 9, 92, 254, 164, 173, 53, 184, 157, 240, 120, 179, 217, 20, 24, 10, 115, 138, 124, 166, 108, 127, 22, 50, 97, 160, 98, 57, 217, 59, 135, 75, 50, 127, 68, 242, 95, 4, 7, 45, 224, 21, 108, 237, 165, 235, 246, 67, 66, 153 }, new byte[] { 242, 111, 80, 153, 101, 172, 255, 86, 166, 190, 229, 66, 73, 51, 120, 219, 74, 42, 252, 30, 208, 94, 37, 19, 63, 103, 24, 6, 53, 127, 95, 250, 149, 88, 239, 96, 36, 88, 46, 196, 111, 200, 249, 220, 237, 83, 88, 246, 64, 74, 46, 85, 203, 195, 255, 251, 220, 56, 94, 21, 57, 28, 134, 98, 248, 108, 246, 243, 204, 105, 120, 53, 177, 161, 39, 226, 59, 58, 102, 89, 199, 162, 37, 213, 165, 118, 231, 78, 237, 107, 198, 232, 117, 187, 34, 16, 153, 91, 177, 219, 97, 197, 104, 143, 14, 191, 80, 233, 230, 220, 94, 94, 250, 238, 226, 51, 13, 2, 212, 29, 175, 111, 95, 126, 137, 230, 202, 58 } });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 178, 131, 134, 128, 77, 218, 51, 216, 233, 248, 205, 76, 101, 138, 249, 105, 61, 180, 220, 104, 144, 20, 177, 170, 162, 242, 214, 88, 228, 174, 128, 167, 80, 18, 101, 29, 154, 12, 206, 154, 48, 149, 92, 80, 113, 162, 254, 247, 81, 115, 201, 4, 172, 42, 71, 151, 26, 193, 36, 214, 10, 42, 60, 94 }, new byte[] { 3, 201, 100, 248, 221, 100, 132, 228, 168, 2, 83, 206, 84, 229, 246, 186, 224, 146, 153, 74, 20, 30, 29, 123, 203, 253, 124, 59, 195, 16, 123, 250, 237, 64, 205, 55, 35, 110, 0, 149, 77, 159, 236, 76, 134, 198, 71, 88, 190, 89, 247, 84, 83, 152, 46, 29, 121, 28, 201, 90, 21, 152, 97, 239, 223, 174, 225, 221, 12, 187, 84, 44, 108, 19, 181, 255, 8, 14, 205, 104, 24, 134, 78, 171, 128, 213, 81, 139, 212, 251, 132, 245, 18, 113, 71, 194, 45, 91, 204, 81, 172, 126, 97, 19, 82, 22, 4, 122, 87, 82, 138, 9, 26, 137, 124, 214, 138, 74, 246, 164, 160, 78, 169, 104, 117, 172, 238, 72 } });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 0, 72, 159, 63, 232, 133, 84, 243, 138, 230, 158, 4, 148, 208, 73, 146, 57, 210, 218, 170, 186, 247, 48, 192, 122, 149, 12, 227, 135, 186, 249, 197, 109, 164, 231, 67, 106, 197, 187, 251, 47, 226, 63, 202, 1, 174, 149, 196, 15, 145, 23, 208, 232, 144, 198, 69, 8, 174, 191, 227, 47, 120, 53, 45 }, new byte[] { 100, 63, 46, 135, 109, 222, 133, 33, 136, 209, 12, 57, 1, 27, 179, 13, 88, 32, 140, 252, 233, 15, 214, 94, 230, 199, 132, 94, 128, 162, 166, 107, 128, 51, 22, 18, 243, 57, 95, 70, 181, 226, 84, 49, 12, 120, 38, 213, 26, 17, 110, 79, 149, 83, 11, 32, 180, 125, 244, 121, 245, 133, 145, 78, 17, 21, 82, 157, 107, 1, 77, 143, 198, 9, 89, 230, 134, 22, 197, 48, 170, 118, 107, 67, 146, 172, 196, 25, 142, 205, 131, 229, 208, 151, 194, 252, 172, 136, 38, 128, 218, 68, 151, 127, 110, 102, 251, 207, 167, 54, 182, 90, 88, 153, 176, 202, 77, 213, 81, 49, 162, 57, 82, 95, 136, 46, 202, 198 } });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 133, 60, 219, 137, 214, 40, 81, 254, 235, 25, 98, 86, 66, 88, 107, 211, 173, 92, 74, 196, 91, 85, 134, 137, 219, 35, 194, 239, 100, 39, 151, 121, 216, 245, 29, 172, 58, 56, 15, 95, 114, 35, 126, 205, 135, 170, 200, 87, 68, 115, 65, 135, 123, 12, 255, 14, 234, 208, 188, 153, 122, 178, 205, 103 }, new byte[] { 144, 243, 194, 134, 154, 8, 63, 126, 72, 204, 194, 169, 24, 28, 127, 194, 77, 125, 222, 228, 244, 81, 2, 235, 132, 43, 209, 6, 74, 157, 129, 189, 9, 249, 17, 76, 77, 246, 114, 31, 157, 126, 185, 210, 199, 67, 199, 138, 196, 217, 202, 0, 206, 227, 51, 123, 104, 72, 170, 195, 56, 157, 211, 83, 115, 76, 113, 240, 161, 185, 95, 139, 7, 115, 218, 46, 95, 4, 185, 44, 28, 199, 57, 245, 153, 240, 18, 125, 189, 135, 249, 165, 51, 27, 93, 199, 216, 46, 63, 186, 168, 68, 253, 107, 60, 183, 114, 102, 119, 203, 117, 92, 15, 206, 39, 37, 34, 172, 6, 194, 9, 198, 137, 130, 80, 225, 50, 127 } });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 31, 82, 152, 159, 228, 99, 70, 100, 233, 149, 116, 194, 53, 179, 240, 186, 131, 58, 64, 64, 188, 117, 216, 112, 134, 155, 247, 51, 174, 124, 41, 134, 170, 217, 4, 145, 53, 71, 29, 170, 209, 215, 175, 93, 107, 230, 145, 214, 3, 154, 164, 147, 20, 124, 69, 70, 97, 79, 141, 68, 102, 171, 87, 0 }, new byte[] { 150, 141, 236, 199, 146, 116, 10, 106, 243, 17, 182, 173, 33, 247, 100, 255, 240, 192, 79, 4, 120, 129, 67, 166, 175, 222, 163, 184, 113, 254, 178, 55, 227, 155, 247, 185, 184, 56, 118, 61, 207, 31, 94, 213, 121, 255, 110, 126, 40, 173, 26, 195, 206, 112, 56, 26, 165, 117, 81, 132, 198, 19, 61, 2, 252, 99, 138, 206, 105, 214, 251, 79, 242, 133, 173, 62, 119, 204, 103, 139, 197, 29, 31, 1, 55, 133, 20, 145, 142, 173, 205, 135, 111, 169, 45, 229, 216, 128, 110, 21, 57, 247, 76, 139, 44, 130, 227, 35, 169, 77, 203, 170, 50, 18, 241, 244, 113, 47, 233, 223, 233, 147, 73, 232, 234, 169, 220, 242 } });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleName",
                value: "user");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoleName",
                value: "moderator");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoleName",
                value: "admin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 98, 41, 10, 213, 219, 170, 121, 193, 254, 126, 189, 191, 108, 124, 136, 248, 229, 85, 191, 150, 126, 10, 9, 0, 190, 26, 63, 10, 87, 173, 85, 163, 77, 138, 167, 251, 221, 20, 111, 75, 196, 51, 212, 55, 198, 8, 73, 32, 255, 7, 207, 220, 42, 97, 11, 116, 34, 218, 191, 39, 252, 240, 31, 231 }, new byte[] { 164, 101, 247, 30, 100, 38, 189, 132, 160, 214, 242, 220, 191, 223, 83, 173, 162, 116, 22, 222, 18, 44, 24, 120, 83, 55, 5, 206, 38, 213, 31, 32, 79, 52, 38, 18, 138, 188, 175, 152, 35, 102, 185, 250, 238, 213, 129, 79, 32, 190, 107, 95, 60, 154, 49, 240, 82, 113, 148, 190, 252, 193, 181, 218, 218, 118, 83, 240, 228, 109, 211, 25, 56, 39, 173, 203, 180, 9, 234, 98, 139, 123, 104, 14, 51, 223, 101, 72, 16, 35, 139, 206, 36, 201, 128, 164, 229, 176, 183, 71, 237, 225, 89, 154, 33, 162, 120, 166, 45, 223, 255, 134, 122, 64, 113, 166, 93, 55, 53, 73, 43, 141, 245, 200, 252, 49, 54, 136 } });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 47, 243, 107, 174, 7, 139, 164, 205, 76, 194, 130, 177, 28, 50, 127, 111, 132, 28, 66, 182, 13, 171, 197, 221, 251, 243, 3, 74, 251, 74, 35, 96, 43, 84, 137, 2, 106, 166, 227, 24, 6, 229, 252, 9, 217, 33, 17, 174, 9, 124, 119, 181, 239, 233, 156, 43, 1, 104, 155, 168, 39, 246, 59, 90 }, new byte[] { 200, 48, 1, 194, 197, 185, 98, 160, 160, 83, 103, 94, 113, 88, 44, 213, 19, 61, 10, 175, 50, 160, 206, 115, 206, 77, 10, 58, 159, 183, 89, 113, 28, 224, 72, 135, 33, 18, 42, 157, 104, 192, 220, 84, 40, 123, 141, 188, 68, 91, 29, 220, 17, 27, 79, 98, 171, 29, 61, 39, 171, 14, 72, 27, 217, 48, 65, 63, 199, 34, 233, 157, 154, 26, 4, 8, 253, 197, 155, 134, 67, 91, 178, 125, 117, 119, 196, 76, 16, 92, 36, 221, 69, 234, 9, 228, 38, 184, 218, 216, 199, 102, 175, 25, 43, 19, 219, 208, 52, 13, 183, 230, 207, 190, 194, 221, 220, 3, 197, 233, 44, 71, 200, 173, 20, 60, 41, 209 } });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 174, 206, 160, 124, 61, 198, 209, 172, 90, 182, 229, 52, 49, 1, 48, 194, 171, 41, 59, 20, 3, 160, 128, 187, 31, 250, 239, 200, 90, 116, 65, 183, 216, 116, 227, 66, 215, 21, 73, 31, 241, 160, 220, 68, 174, 48, 72, 48, 71, 244, 147, 148, 185, 236, 153, 79, 10, 105, 41, 232, 42, 188, 4, 216 }, new byte[] { 236, 61, 58, 198, 119, 225, 175, 244, 46, 19, 26, 166, 103, 92, 141, 52, 101, 136, 203, 201, 157, 210, 219, 255, 45, 128, 125, 0, 253, 164, 74, 90, 87, 166, 227, 44, 124, 106, 197, 92, 195, 117, 44, 225, 36, 33, 8, 227, 159, 221, 229, 184, 173, 59, 206, 25, 44, 249, 214, 105, 156, 68, 237, 199, 130, 190, 63, 223, 91, 40, 220, 182, 192, 121, 190, 94, 216, 137, 249, 252, 86, 127, 88, 146, 66, 208, 40, 4, 145, 20, 220, 187, 64, 191, 146, 186, 126, 235, 17, 145, 226, 178, 163, 210, 69, 19, 69, 144, 60, 253, 131, 10, 3, 1, 221, 151, 1, 185, 242, 20, 159, 186, 207, 115, 150, 126, 227, 97 } });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 85, 84, 126, 75, 150, 104, 217, 46, 176, 180, 180, 223, 58, 65, 148, 196, 131, 12, 1, 142, 44, 7, 19, 148, 186, 193, 3, 129, 98, 230, 175, 111, 39, 244, 183, 72, 48, 184, 193, 45, 79, 218, 164, 80, 223, 91, 58, 49, 124, 38, 193, 92, 76, 117, 116, 51, 35, 235, 88, 135, 99, 144, 190, 9 }, new byte[] { 42, 120, 61, 126, 103, 44, 40, 60, 207, 212, 200, 224, 67, 232, 224, 219, 212, 250, 202, 152, 254, 227, 144, 145, 47, 117, 224, 30, 17, 168, 243, 32, 154, 164, 140, 198, 82, 155, 126, 196, 41, 154, 168, 84, 28, 73, 1, 247, 150, 21, 160, 186, 7, 141, 127, 0, 85, 211, 215, 11, 245, 6, 106, 212, 44, 177, 47, 23, 64, 254, 169, 156, 48, 12, 18, 82, 203, 39, 142, 190, 252, 186, 119, 88, 236, 245, 48, 4, 217, 24, 46, 113, 140, 54, 112, 31, 105, 193, 2, 17, 173, 222, 197, 41, 27, 135, 233, 16, 183, 150, 150, 86, 108, 152, 167, 178, 67, 14, 138, 103, 194, 133, 213, 129, 159, 28, 90, 190 } });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 168, 96, 246, 82, 122, 163, 18, 166, 62, 201, 154, 209, 143, 65, 113, 62, 16, 203, 16, 4, 30, 199, 65, 87, 75, 219, 136, 186, 81, 251, 52, 121, 211, 61, 14, 134, 251, 81, 191, 172, 48, 77, 115, 51, 5, 238, 249, 217, 241, 87, 51, 4, 185, 114, 207, 129, 181, 19, 71, 29, 223, 140, 88, 180 }, new byte[] { 14, 138, 44, 139, 159, 19, 95, 103, 165, 14, 88, 202, 134, 164, 102, 14, 36, 54, 19, 15, 118, 45, 143, 175, 76, 56, 90, 84, 148, 2, 183, 134, 56, 149, 114, 64, 76, 117, 103, 126, 60, 24, 156, 189, 105, 119, 25, 195, 102, 134, 233, 43, 176, 53, 55, 159, 99, 67, 22, 188, 80, 90, 37, 227, 120, 143, 197, 33, 46, 133, 3, 247, 220, 46, 129, 39, 72, 223, 127, 168, 111, 148, 34, 5, 95, 171, 167, 181, 216, 103, 35, 25, 119, 161, 184, 165, 92, 213, 213, 207, 182, 148, 156, 48, 170, 32, 49, 109, 5, 28, 165, 50, 12, 171, 236, 196, 205, 168, 38, 189, 174, 32, 106, 88, 134, 227, 5, 9 } });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "RoleName",
                value: "User");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "RoleName",
                value: "Moderator");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "RoleName",
                value: "Admin");
        }
    }
}