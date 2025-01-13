using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderMs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginLocation",
                table: "Order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Verified",
                table: "AdditionalCost",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginLocation",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "AdditionalCost");
        }
    }
}
