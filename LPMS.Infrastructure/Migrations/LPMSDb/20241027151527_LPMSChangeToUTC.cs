using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LPMS.Infrastructure.Migrations.LPMSDb
{
    /// <inheritdoc />
    public partial class LPMSChangeToUTC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                schema: "core",
                table: "ReferenceType",
                newName: "ModifiedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                schema: "core",
                table: "ReferenceType",
                newName: "CreatedOnUTC");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                schema: "core",
                table: "Reference",
                newName: "ModifiedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                schema: "core",
                table: "Reference",
                newName: "CreatedOnUTC");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "EndpointxSystemRole",
                newName: "ModifiedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "EndpointxSystemRole",
                newName: "CreatedOnUTC");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "EndpointOperation",
                newName: "ModifiedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "EndpointOperation",
                newName: "CreatedOnUTC");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "Endpoint",
                newName: "ModifiedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Endpoint",
                newName: "CreatedOnUTC");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                table: "EmailHistory",
                newName: "ModifiedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "EmailHistory",
                newName: "CreatedOnUTC");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                schema: "core",
                table: "Division",
                newName: "ModifiedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                schema: "core",
                table: "Division",
                newName: "CreatedOnUTC");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                schema: "core",
                table: "Department",
                newName: "ModifiedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                schema: "core",
                table: "Department",
                newName: "CreatedOnUTC");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                schema: "core",
                table: "Country",
                newName: "ModifiedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                schema: "core",
                table: "Country",
                newName: "CreatedOnUTC");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                schema: "core",
                table: "Client",
                newName: "ModifiedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                schema: "core",
                table: "Client",
                newName: "CreatedOnUTC");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                schema: "core",
                table: "City",
                newName: "ModifiedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                schema: "core",
                table: "City",
                newName: "CreatedOnUTC");

            migrationBuilder.RenameColumn(
                name: "ModifiedOn",
                schema: "core",
                table: "Account",
                newName: "ModifiedOnUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                schema: "core",
                table: "Account",
                newName: "CreatedOnUTC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedOnUTC",
                schema: "core",
                table: "ReferenceType",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                schema: "core",
                table: "ReferenceType",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUTC",
                schema: "core",
                table: "Reference",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                schema: "core",
                table: "Reference",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUTC",
                table: "EndpointxSystemRole",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                table: "EndpointxSystemRole",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUTC",
                table: "EndpointOperation",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                table: "EndpointOperation",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUTC",
                table: "Endpoint",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                table: "Endpoint",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUTC",
                table: "EmailHistory",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                table: "EmailHistory",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUTC",
                schema: "core",
                table: "Division",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                schema: "core",
                table: "Division",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUTC",
                schema: "core",
                table: "Department",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                schema: "core",
                table: "Department",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUTC",
                schema: "core",
                table: "Country",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                schema: "core",
                table: "Country",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUTC",
                schema: "core",
                table: "Client",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                schema: "core",
                table: "Client",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUTC",
                schema: "core",
                table: "City",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                schema: "core",
                table: "City",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "ModifiedOnUTC",
                schema: "core",
                table: "Account",
                newName: "ModifiedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedOnUTC",
                schema: "core",
                table: "Account",
                newName: "CreatedOn");
        }
    }
}
