using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStaffTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Combos_ComboId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_ComboId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "ComboId",
                table: "Foods");

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdStore = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Staffs_Stores_IdStore",
                        column: x => x.IdStore,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComboItems_IdCombo",
                table: "ComboItems",
                column: "IdCombo");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_IdStore",
                table: "Staffs",
                column: "IdStore");

            migrationBuilder.AddForeignKey(
                name: "FK_ComboItems_Combos_IdCombo",
                table: "ComboItems",
                column: "IdCombo",
                principalTable: "Combos",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_ComboItems_Foods_IdCombo",
                table: "ComboItems",
                column: "IdCombo",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComboItems_Combos_IdCombo",
                table: "ComboItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ComboItems_Foods_IdCombo",
                table: "ComboItems");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_ComboItems_IdCombo",
                table: "ComboItems");

            migrationBuilder.AddColumn<int>(
                name: "ComboId",
                table: "Foods",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdInvoice",
                table: "Orders",
                column: "IdInvoice");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_ComboId",
                table: "Foods",
                column: "ComboId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Combos_ComboId",
                table: "Foods",
                column: "ComboId",
                principalTable: "Combos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Invoices_IdInvoice",
                table: "Orders",
                column: "IdInvoice",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
