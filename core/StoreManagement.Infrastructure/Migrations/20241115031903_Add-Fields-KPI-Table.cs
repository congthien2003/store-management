using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsKPITable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "KPIs",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "KPIs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "KPIs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "KPIs");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "KPIs");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "KPIs",
                newName: "Date");
        }
    }
}
