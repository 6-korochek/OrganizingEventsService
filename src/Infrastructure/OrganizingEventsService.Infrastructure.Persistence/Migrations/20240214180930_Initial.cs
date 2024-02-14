using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizingEventsService.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("Npgsql:Enum:event_participant_invite_status", "accepted,declined,pending")
            .Annotation("Npgsql:Enum:event_status", "in_draft,planed,is_over,in_going");

        migrationBuilder.CreateTable(
            name: "account",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                surname = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                email = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                is_invite = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
            },
            constraints: table =>
            {
                table.PrimaryKey("account_pkey", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "event",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                description = table.Column<string>(type: "text", nullable: true),
                max_participant = table.Column<decimal>(type: "numeric(4)", precision: 4, nullable: false),
                meeting_link = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                start_datetime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                end_datetime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                status = table.Column<string>(type: "text", nullable: false, defaultValue: "InDraft"),
                invite_code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("event_pkey", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "permission",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                description = table.Column<string>(type: "text", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("permission_pkey", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "role",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("role_pkey", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "event_participant",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                event_pk = table.Column<Guid>(type: "uuid", nullable: false),
                account_pk = table.Column<Guid>(type: "uuid", nullable: false),
                invite_status = table.Column<string>(type: "text", nullable: false),
                is_banned = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
                role_pk = table.Column<Guid>(type: "uuid", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("event_participant_pkey", x => x.id);
                table.ForeignKey(
                    name: "event_participant_account_pk_fkey",
                    column: x => x.account_pk,
                    principalTable: "account",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "event_participant_event_pk_fkey",
                    column: x => x.event_pk,
                    principalTable: "event",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "event_participant_role_pk_fkey",
                    column: x => x.role_pk,
                    principalTable: "role",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "role_permission",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                role_pk = table.Column<Guid>(type: "uuid", nullable: false),
                permission_pk = table.Column<Guid>(type: "uuid", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("role_permission_pkey", x => x.id);
                table.ForeignKey(
                    name: "role_permission_permission_pk_fkey",
                    column: x => x.permission_pk,
                    principalTable: "permission",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "role_permission_role_pk_fkey",
                    column: x => x.role_pk,
                    principalTable: "role",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "feedback",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                event_participant_pk = table.Column<Guid>(type: "uuid", nullable: false),
                rating = table.Column<decimal>(type: "numeric(1)", precision: 1, nullable: false),
                text = table.Column<string>(type: "text", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("feedback_pkey", x => x.id);
                table.ForeignKey(
                    name: "feedback_event_participant_pk_fkey",
                    column: x => x.event_participant_pk,
                    principalTable: "event_participant",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "account_email_key",
            table: "account",
            column: "email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "event_invite_code_key",
            table: "event",
            column: "invite_code",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_event_participant_account_pk",
            table: "event_participant",
            column: "account_pk");

        migrationBuilder.CreateIndex(
            name: "IX_event_participant_event_pk",
            table: "event_participant",
            column: "event_pk");

        migrationBuilder.CreateIndex(
            name: "IX_event_participant_role_pk",
            table: "event_participant",
            column: "role_pk");

        migrationBuilder.CreateIndex(
            name: "IX_feedback_event_participant_pk",
            table: "feedback",
            column: "event_participant_pk");

        migrationBuilder.CreateIndex(
            name: "permission_name_key",
            table: "permission",
            column: "name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "role_name_key",
            table: "role",
            column: "name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_role_permission_permission_pk",
            table: "role_permission",
            column: "permission_pk");

        migrationBuilder.CreateIndex(
            name: "IX_role_permission_role_pk",
            table: "role_permission",
            column: "role_pk");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "feedback");

        migrationBuilder.DropTable(
            name: "role_permission");

        migrationBuilder.DropTable(
            name: "event_participant");

        migrationBuilder.DropTable(
            name: "permission");

        migrationBuilder.DropTable(
            name: "account");

        migrationBuilder.DropTable(
            name: "event");

        migrationBuilder.DropTable(
            name: "role");

        migrationBuilder.Sql("DROP TYPE event_participant_invite_status;");
        migrationBuilder.Sql("DROP TYPE event_status;");
    }
}
