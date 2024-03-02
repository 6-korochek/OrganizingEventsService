using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizingEventsService.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class AddedIsArchiveIsAdmin : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "rating",
            table: "feedback",
            type: "numeric(1)",
            precision: 1,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(1,0)",
            oldPrecision: 1);

        migrationBuilder.AddColumn<bool>(
            name: "is_archive",
            table: "event_participant",
            type: "boolean",
            nullable: true,
            defaultValueSql: "false");

        migrationBuilder.AlterColumn<decimal>(
            name: "max_participant",
            table: "event",
            type: "numeric(4)",
            precision: 4,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(4,0)",
            oldPrecision: 4);

        migrationBuilder.AddColumn<bool>(
            name: "is_admin",
            table: "account",
            type: "boolean",
            nullable: true,
            defaultValueSql: "false");

        migrationBuilder.AddColumn<DateTime>(
            name: "password_hash_created_at",
            table: "account",
            type: "timestamp without time zone",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "is_archive",
            table: "event_participant");

        migrationBuilder.DropColumn(
            name: "is_admin",
            table: "account");

        migrationBuilder.DropColumn(
            name: "password_hash_created_at",
            table: "account");

        migrationBuilder.AlterColumn<decimal>(
            name: "rating",
            table: "feedback",
            type: "numeric(1,0)",
            precision: 1,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(1)",
            oldPrecision: 1);

        migrationBuilder.AlterColumn<decimal>(
            name: "max_participant",
            table: "event",
            type: "numeric(4,0)",
            precision: 4,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(4)",
            oldPrecision: 4);
    }
}
