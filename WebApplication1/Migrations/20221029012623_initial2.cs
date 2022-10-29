using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_EmployeesId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "EmployeesId",
                table: "Users",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_EmployeesId",
                table: "Users",
                newName: "IX_Users_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_EmployeeId",
                table: "Users",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_EmployeeId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Users",
                newName: "EmployeesId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_EmployeeId",
                table: "Users",
                newName: "IX_Users_EmployeesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_EmployeesId",
                table: "Users",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
