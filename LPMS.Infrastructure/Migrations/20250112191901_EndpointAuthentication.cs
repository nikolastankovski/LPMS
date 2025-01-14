using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LPMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EndpointAuthentication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndpointOperation",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "Action",
                schema: "dbo",
                table: "Endpoint");

            migrationBuilder.DropColumn(
                name: "Controller",
                schema: "dbo",
                table: "Endpoint");

            migrationBuilder.DropColumn(
                name: "FullPath",
                schema: "dbo",
                table: "Endpoint");

            migrationBuilder.DropColumn(
                name: "Method",
                schema: "dbo",
                table: "Endpoint");

            migrationBuilder.RenameColumn(
                name: "Route",
                schema: "dbo",
                table: "Endpoint",
                newName: "RequestPath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestPath",
                schema: "dbo",
                table: "Endpoint",
                newName: "Route");

            migrationBuilder.AddColumn<string>(
                name: "Action",
                schema: "dbo",
                table: "Endpoint",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Controller",
                schema: "dbo",
                table: "Endpoint",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullPath",
                schema: "dbo",
                table: "Endpoint",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Method",
                schema: "dbo",
                table: "Endpoint",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EndpointOperation",
                schema: "dbo",
                columns: table => new
                {
                    EndpointOperationID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EndpointId = table.Column<int>(type: "integer", nullable: false),
                    Create = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    Delete = table.Column<bool>(type: "boolean", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Read = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Update = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndpointOperation_EndpointOperationID", x => x.EndpointOperationID);
                    table.ForeignKey(
                        name: "FK_EndpointOperation_Endpoint",
                        column: x => x.EndpointId,
                        principalSchema: "dbo",
                        principalTable: "Endpoint",
                        principalColumn: "EndpointID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EndpointOperation_EndpointId",
                schema: "dbo",
                table: "EndpointOperation",
                column: "EndpointId");
        }
    }
}
