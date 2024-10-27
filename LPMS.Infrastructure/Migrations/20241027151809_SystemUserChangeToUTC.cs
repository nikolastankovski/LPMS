using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LPMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SystemUserChangeToUTC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                schema: "dbo",
                table: "SystemRole",
                newName: "ModifiedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                schema: "dbo",
                table: "SystemRole",
                newName: "CreatedOnUTC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedOnUTC",
                schema: "dbo",
                table: "SystemRole",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                schema: "dbo",
                table: "SystemRole",
                newName: "CreatedOn");
        }
    }
}
