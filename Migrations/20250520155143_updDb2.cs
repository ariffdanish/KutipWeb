using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutipWeb.Migrations
{
    /// <inheritdoc />
    public partial class updDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfterPhotoUrl",
                table: "Pickups");

            migrationBuilder.DropColumn(
                name: "BeforePhotoUrl",
                table: "Pickups");

            migrationBuilder.RenameColumn(
                name: "DuringPhotoUrl",
                table: "Pickups",
                newName: "PhotoUrl");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Bins",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Bins",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Pickups",
                newName: "DuringPhotoUrl");

            migrationBuilder.AddColumn<string>(
                name: "AfterPhotoUrl",
                table: "Pickups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BeforePhotoUrl",
                table: "Pickups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Bins",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Bins",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
