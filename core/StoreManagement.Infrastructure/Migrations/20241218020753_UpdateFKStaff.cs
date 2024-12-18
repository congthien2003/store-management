using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFKStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Stores_IdStore",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_IdStore",
                table: "Staffs");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_IdStore",
                table: "Staffs",
                column: "IdStore");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Stores_IdStore",
                table: "Staffs",
                column: "IdStore",
                principalTable: "Stores",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Stores_IdStore",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_IdStore",
                table: "Staffs");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_IdStore",
                table: "Staffs",
                column: "IdStore",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Stores_IdStore",
                table: "Staffs",
                column: "IdStore",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
