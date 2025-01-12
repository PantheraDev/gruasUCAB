using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProviderMs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FourthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderDepartments_Department_DepartmentId1",
                table: "ProviderDepartments");

            migrationBuilder.DropIndex(
                name: "IX_ProviderDepartments_DepartmentId1",
                table: "ProviderDepartments");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "ProviderDepartments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId1",
                table: "ProviderDepartments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProviderDepartments_DepartmentId1",
                table: "ProviderDepartments",
                column: "DepartmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderDepartments_Department_DepartmentId1",
                table: "ProviderDepartments",
                column: "DepartmentId1",
                principalTable: "Department",
                principalColumn: "Id");
        }
    }
}
