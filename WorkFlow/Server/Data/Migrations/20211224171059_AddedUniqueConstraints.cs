using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkFlow.Server.Migrations
{
    public partial class AddedUniqueConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserCompany_UserId",
                table: "UserCompany");

            migrationBuilder.AlterColumn<string>(
                name: "Uri",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Uri",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompany_UserId_CompanyId",
                table: "UserCompany",
                columns: new[] { "UserId", "CompanyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Uri",
                table: "Projects",
                column: "Uri",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Uri",
                table: "Companies",
                column: "Uri",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserCompany_UserId_CompanyId",
                table: "UserCompany");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Uri",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Companies_Uri",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "Uri",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Uri",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompany_UserId",
                table: "UserCompany",
                column: "UserId");
        }
    }
}
