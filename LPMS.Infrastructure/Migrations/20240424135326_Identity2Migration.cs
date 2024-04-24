using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LPMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Identity2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ApplicationRole",
                newName: "Name_EN");

            migrationBuilder.AddColumn<string>(
                name: "Name_MK",
                table: "ApplicationRole",
                type: "nvarchar(256)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name_MK",
                table: "ApplicationRole");

            migrationBuilder.RenameColumn(
                name: "Name_EN",
                table: "ApplicationRole",
                newName: "Name");
        }
    }
}
