using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizingEventsService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsAndDeletePermissionAndRolePermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_permission");

            migrationBuilder.DropTable(
                name: "permission");














            migrationBuilder.AlterColumn<decimal>(
                name: "rating",
                table: "feedback",
                type: "numeric(1)",
                precision: 1,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(1,0)",
                oldPrecision: 1);

            migrationBuilder.AlterColumn<bool>(
                name: "is_banned",
                table: "event_participant",
                type: "boolean",
                nullable: false,
                defaultValueSql: "false",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValueSql: "false");

            migrationBuilder.AlterColumn<decimal>(
                name: "max_participant",
                table: "event",
                type: "numeric(4)",
                precision: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(4,0)",
                oldPrecision: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "event_participant_pk",
                table: "feedback",
                newName: "event_participant_id");

            migrationBuilder.RenameIndex(
                name: "IX_feedback_event_participant_pk",
                table: "feedback",
                newName: "IX_feedback_event_participant_id");

            migrationBuilder.RenameColumn(
                name: "role_pk",
                table: "event_participant",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "event_pk",
                table: "event_participant",
                newName: "event_id");

            migrationBuilder.RenameColumn(
                name: "account_pk",
                table: "event_participant",
                newName: "account_id");

            migrationBuilder.RenameIndex(
                name: "IX_event_participant_role_pk",
                table: "event_participant",
                newName: "IX_event_participant_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_event_participant_event_pk",
                table: "event_participant",
                newName: "IX_event_participant_event_id");

            migrationBuilder.RenameIndex(
                name: "IX_event_participant_account_pk",
                table: "event_participant",
                newName: "IX_event_participant_account_id");

            migrationBuilder.AlterColumn<decimal>(
                name: "rating",
                table: "feedback",
                type: "numeric(1,0)",
                precision: 1,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(1)",
                oldPrecision: 1);

            migrationBuilder.AlterColumn<bool>(
                name: "is_banned",
                table: "event_participant",
                type: "boolean",
                nullable: true,
                defaultValueSql: "false",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValueSql: "false");

            migrationBuilder.AlterColumn<decimal>(
                name: "max_participant",
                table: "event",
                type: "numeric(4,0)",
                precision: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(4)",
                oldPrecision: 4);

            migrationBuilder.CreateTable(
                name: "permission",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("permission_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role_permission",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("role_permission_pkey", x => x.id);
                    table.ForeignKey(
                        name: "role_permission_permission_pk_fkey",
                        column: x => x.permission_id,
                        principalTable: "permission",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "role_permission_role_pk_fkey",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "permission_name_key",
                table: "permission",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_permission_id",
                table: "role_permission",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_role_id",
                table: "role_permission",
                column: "role_id");
        }
    }
}
