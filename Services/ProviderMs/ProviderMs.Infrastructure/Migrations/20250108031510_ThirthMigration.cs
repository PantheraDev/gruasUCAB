using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProviderMs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ThirthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepartmentProvider");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId1",
                table: "ProviderDepartments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProviderDepartments_DepartmentId1",
                table: "ProviderDepartments",
                column: "DepartmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderDepartments_ProviderId",
                table: "ProviderDepartments",
                column: "ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderDepartments_Department_DepartmentId1",
                table: "ProviderDepartments",
                column: "DepartmentId1",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderDepartments_Provider_ProviderId",
                table: "ProviderDepartments",
                column: "ProviderId",
                principalTable: "Provider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProviderDepartments_Department_DepartmentId1",
                table: "ProviderDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderDepartments_Provider_ProviderId",
                table: "ProviderDepartments");

            migrationBuilder.DropIndex(
                name: "IX_ProviderDepartments_DepartmentId1",
                table: "ProviderDepartments");

            migrationBuilder.DropIndex(
                name: "IX_ProviderDepartments_ProviderId",
                table: "ProviderDepartments");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "ProviderDepartments");

            migrationBuilder.CreateTable(
                name: "DepartmentProvider",
                columns: table => new
                {
                    DepartmentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProvidersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentProvider", x => new { x.DepartmentsId, x.ProvidersId });
                    table.ForeignKey(
                        name: "FK_DepartmentProvider_Department_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentProvider_Provider_ProvidersId",
                        column: x => x.ProvidersId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentProvider_ProvidersId",
                table: "DepartmentProvider",
                column: "ProvidersId");
        }
    }
}
