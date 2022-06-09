using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations.AuthenticationDb
{
    public partial class DeletedAccountIdReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "AccountAuths");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "AccountAuths",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
