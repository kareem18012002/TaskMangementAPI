using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskMangementAPI.Migrations
{
    /// <inheritdoc />
    public partial class dto_task : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isdBy",
                table: "taskItems");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "taskItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "taskItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "taskItems");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "taskItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "isdBy",
                table: "taskItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
