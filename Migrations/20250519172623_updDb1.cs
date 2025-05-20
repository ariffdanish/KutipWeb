using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutipWeb.Migrations
{
    /// <inheritdoc />
    public partial class updDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pickups_BinId",
                table: "Pickups",
                column: "BinId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pickups_Bins_BinId",
                table: "Pickups",
                column: "BinId",
                principalTable: "Bins",
                principalColumn: "BinId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pickups_Bins_BinId",
                table: "Pickups");

            migrationBuilder.DropIndex(
                name: "IX_Pickups_BinId",
                table: "Pickups");
        }
    }
}
