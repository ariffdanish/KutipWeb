using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutipWeb.Migrations
{
    /// <inheritdoc />
    public partial class updateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collectors_Pickups_PickupId",
                table: "Collectors");

            migrationBuilder.DropIndex(
                name: "IX_Collectors_PickupId",
                table: "Collectors");

            migrationBuilder.DropColumn(
                name: "PickupId",
                table: "Collectors");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Pickups",
                newName: "status");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PlateID",
                table: "Bins",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Bins",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Pickups_CollectorId",
                table: "Pickups",
                column: "CollectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pickups_Collectors_CollectorId",
                table: "Pickups",
                column: "CollectorId",
                principalTable: "Collectors",
                principalColumn: "CollectorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pickups_Collectors_CollectorId",
                table: "Pickups");

            migrationBuilder.DropIndex(
                name: "IX_Pickups_CollectorId",
                table: "Pickups");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Pickups",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "PickupId",
                table: "Collectors",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PlateID",
                table: "Bins",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "Bins",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Collectors_PickupId",
                table: "Collectors",
                column: "PickupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collectors_Pickups_PickupId",
                table: "Collectors",
                column: "PickupId",
                principalTable: "Pickups",
                principalColumn: "PickupId");
        }
    }
}
