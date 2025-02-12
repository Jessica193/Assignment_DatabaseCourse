using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class MovedQuantityToProjectsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "QuantityofServiceUnits",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityofServiceUnits",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Services",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
