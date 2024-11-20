using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LPMS.Infrastructure.Migrations.SystemUserDb
{
    /// <inheritdoc />
    public partial class MigrateToPostgreSystemDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "SystemRole",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayName_EN = table.Column<string>(type: "varchar(256)", nullable: false),
                    DisplayName_MK = table.Column<string>(type: "varchar(256)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOnUTC = table.Column<DateTime>(type: "TIMESTAMP(3)", nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedOnUTC = table.Column<DateTime>(type: "TIMESTAMP(3)", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemUser",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PasswordChangePeriodInMonths = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "12"),
                    LastPasswordChange = table.Column<DateTime>(type: "TIMESTAMP(3)", nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    LastLogin = table.Column<DateTime>(type: "TIMESTAMP(3)", nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')"),
                    RefreshToken = table.Column<string>(type: "varchar(256)", nullable: true),
                    RefreshTokenExpiresUTC = table.Column<DateTime>(type: "TIMESTAMP(3)", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemRoleClaim",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemRoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemRoleClaim_SystemRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "SystemRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemUserClaim",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemUserClaim_SystemUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "SystemUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemUserLogin",
                schema: "dbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_SystemUserLogin_SystemUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "SystemUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemUserRole",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TIMESTAMP(3)", nullable: false, defaultValueSql: "(NOW() AT TIME ZONE 'UTC')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_SystemUserRole_SystemRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "SystemRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemUserRole_SystemUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "SystemUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemUserToken",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemUserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_SystemUserToken_SystemUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "SystemUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "dbo",
                table: "SystemRole",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemRoleClaim_RoleId",
                schema: "dbo",
                table: "SystemRoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "SystemUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "dbo",
                table: "SystemUser",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserClaim_UserId",
                schema: "dbo",
                table: "SystemUserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserLogin_UserId",
                schema: "dbo",
                table: "SystemUserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemUserRole_RoleId",
                schema: "dbo",
                table: "SystemUserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemRoleClaim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SystemUserClaim",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SystemUserLogin",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SystemUserRole",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SystemUserToken",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SystemRole",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "SystemUser",
                schema: "dbo");
        }
    }
}
