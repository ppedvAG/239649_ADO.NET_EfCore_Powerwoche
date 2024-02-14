using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HalloEfCore_CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class TischMitLampeMitStuhl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MitLampe",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Schreibtischnummer",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Stuhl",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MitLampe",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Schreibtischnummer",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Stuhl",
                table: "Employees");
        }
    }
}
