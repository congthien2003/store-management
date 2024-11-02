using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGuidDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameUser",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PhoneUser",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Tables",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Stores",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ProductSells",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ProductSells");

            migrationBuilder.AddColumn<string>(
                name: "NameUser",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneUser",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
