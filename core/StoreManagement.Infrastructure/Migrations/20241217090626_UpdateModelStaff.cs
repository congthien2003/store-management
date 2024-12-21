using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Staffs_IdStore",
                table: "Staffs");

            migrationBuilder.AddColumn<int>(
                name: "IdUser",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_IdStore",
                table: "Staffs",
                column: "IdStore",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_IdUser",
                table: "Staffs",
                column: "IdUser",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Users_IdUser",
                table: "Staffs",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Users_IdUser",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_IdStore",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_IdUser",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Staffs");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_IdStore",
                table: "Staffs",
                column: "IdStore");
        }
    }
}
