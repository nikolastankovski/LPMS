﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LPMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "core",
                columns: table => new
                {
                    AccountID = table.Column<Guid>(type: "uuid", nullable: false),
                    SystemUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account_AccountID", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                schema: "core",
                columns: table => new
                {
                    ClientID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    ClientTypeId = table.Column<int>(type: "integer", nullable: false),
                    GenderId = table.Column<int>(type: "integer", nullable: true),
                    NationalityId = table.Column<int>(type: "integer", nullable: true),
                    IdDocumentTypeId = table.Column<int>(type: "integer", nullable: true),
                    IdDocumentNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    IdDocumentExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    LegalName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    TradeName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    UniqueIdentificationNumber = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: true),
                    EstablishDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Address2 = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CityId = table.Column<int>(type: "integer", nullable: true),
                    CountryId = table.Column<int>(type: "integer", nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email2 = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Phone2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client_ClientID", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "core",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_EN = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Name_MK = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country_CountryID", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Division",
                schema: "core",
                columns: table => new
                {
                    DivisionID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_EN = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Name_MK = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Division_DivisionID", x => x.DivisionID);
                });

            migrationBuilder.CreateTable(
                name: "EmailHistory",
                schema: "dbo",
                columns: table => new
                {
                    EmailHistoryID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Template = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    From = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    To = table.Column<string>(type: "text", nullable: false),
                    CC = table.Column<string>(type: "text", nullable: true),
                    BCC = table.Column<string>(type: "text", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: false),
                    Attachments = table.Column<string>(type: "text", nullable: true),
                    IsSent = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailHistory_EmailHistoryID", x => x.EmailHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "Endpoint",
                schema: "dbo",
                columns: table => new
                {
                    EndpointID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Controller = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Action = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Method = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Route = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    FullPath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endpoint_EndpointID", x => x.EndpointID);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceType",
                schema: "core",
                columns: table => new
                {
                    ReferenceTypeID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name_EN = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Name_MK = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description_EN = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Description_MK = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceType_ReferenceID", x => x.ReferenceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "City",
                schema: "core",
                columns: table => new
                {
                    CityID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    Name_EN = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Name_MK = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City_CityID", x => x.CityID);
                    table.ForeignKey(
                        name: "FK_City_Country",
                        column: x => x.CountryId,
                        principalSchema: "core",
                        principalTable: "Country",
                        principalColumn: "CountryID");
                });

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "core",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DivisionId = table.Column<int>(type: "integer", nullable: false),
                    Name_EN = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Name_MK = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department_DeparmentID", x => x.DepartmentID);
                    table.ForeignKey(
                        name: "FK_Department_Division",
                        column: x => x.DivisionId,
                        principalSchema: "core",
                        principalTable: "Division",
                        principalColumn: "DivisionID");
                });

            migrationBuilder.CreateTable(
                name: "EndpointOperation",
                schema: "dbo",
                columns: table => new
                {
                    EndpointOperationID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EndpointId = table.Column<int>(type: "integer", nullable: false),
                    Create = table.Column<bool>(type: "boolean", nullable: false),
                    Read = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Update = table.Column<bool>(type: "boolean", nullable: false),
                    Delete = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "EndpointxSystemRole",
                schema: "dbo",
                columns: table => new
                {
                    EndpointxSystemRoleID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EndpointId = table.Column<int>(type: "integer", nullable: false),
                    SystemRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedOnUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndpointxSystemRole_EndpointxSystemRoleID", x => x.EndpointxSystemRoleID);
                    table.ForeignKey(
                        name: "FK_EndpointxSystemRole_Endpoint",
                        column: x => x.EndpointId,
                        principalSchema: "dbo",
                        principalTable: "Endpoint",
                        principalColumn: "EndpointID");
                });

            migrationBuilder.CreateTable(
                name: "Reference",
                schema: "core",
                columns: table => new
                {
                    ReferenceID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReferenceTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name_EN = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Name_MK = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description_EN = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Description_MK = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedOnUTC = table.Column<DateTime>(type: "timestamp(3) with time zone", precision: 3, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reference_ReferenceID", x => x.ReferenceID);
                    table.ForeignKey(
                        name: "FK_Reference_ReferenceType",
                        column: x => x.ReferenceTypeId,
                        principalSchema: "core",
                        principalTable: "ReferenceType",
                        principalColumn: "ReferenceTypeID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                schema: "core",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_DivisionId",
                schema: "core",
                table: "Department",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_EndpointOperation_EndpointId",
                schema: "dbo",
                table: "EndpointOperation",
                column: "EndpointId");

            migrationBuilder.CreateIndex(
                name: "IX_EndpointxSystemRole_EndpointId",
                schema: "dbo",
                table: "EndpointxSystemRole",
                column: "EndpointId");

            migrationBuilder.CreateIndex(
                name: "IX_Reference_Code",
                schema: "core",
                table: "Reference",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Reference_ReferenceTypeId",
                schema: "core",
                table: "Reference",
                column: "ReferenceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceType_Code",
                schema: "core",
                table: "ReferenceType",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account",
                schema: "core");

            migrationBuilder.DropTable(
                name: "City",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Client",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "core");

            migrationBuilder.DropTable(
                name: "EmailHistory",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EndpointOperation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EndpointxSystemRole",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Reference",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Division",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Endpoint",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ReferenceType",
                schema: "core");
        }
    }
}
