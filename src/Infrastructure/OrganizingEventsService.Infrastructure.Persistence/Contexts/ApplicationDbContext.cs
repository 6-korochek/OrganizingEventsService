#pragma warning disable SA1206

using Microsoft.EntityFrameworkCore;
using OrganizingEventsService.Infrastructure.Persistence.Models;
using OrganizingEventsService.Infrastructure.Persistence.Models.Enums;

namespace OrganizingEventsService.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public required DbSet<AccountModel> Accounts { get; set; }

    public required DbSet<EventModel> Events { get; set; }

    public required DbSet<EventParticipantModel> EventParticipants { get; set; }

    public required DbSet<FeedbackModel> Feedbacks { get; set; }

    public required DbSet<PermissionModel> Permissions { get; set; }

    public required DbSet<RoleModel> Roles { get; set; }

    public required DbSet<RolePermissionModel> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum<EventStatus>()
            .HasPostgresEnum<EventParticipantInviteStatus>();

        modelBuilder.Entity<AccountModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_pkey");

            entity.ToTable("account");

            entity.HasIndex(e => e.Email, "account_email_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.PasswordHashUpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("password_hash_created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(70)
                .HasColumnName("email");
            entity.Property(e => e.IsInvite)
                .HasDefaultValueSql("true")
                .HasColumnName("is_invite");
            entity.Property(e => e.IsAdmin)
                .HasDefaultValueSql("false")
                .HasColumnName("is_admin");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Surname)
                .HasMaxLength(70)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<EventModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("event_pkey");

            entity.ToTable("event");

            entity.HasIndex(e => e.InviteCode, "event_invite_code_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EndDatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_datetime");
            entity.Property(e => e.InviteCode)
                .HasMaxLength(15)
                .HasColumnName("invite_code");
            entity.Property(e => e.MaxParticipant)
                .HasPrecision(4)
                .HasColumnName("max_participant");
            entity.Property(e => e.MeetingLink)
                .HasMaxLength(255)
                .HasColumnName("meeting_link");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
            entity.Property(e => e.StartDatetime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_datetime");
            entity.Property(e => e.Status)
                .HasColumnName("status")
                .HasDefaultValue(EventStatus.InDraft)
                .HasConversion<string>();
        });

        modelBuilder.Entity<EventParticipantModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("event_participant_pkey");

            entity.ToTable("event_participant");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_pk");
            entity.Property(e => e.EventId).HasColumnName("event_pk");
            entity.Property(e => e.IsBanned)
                .HasDefaultValueSql("false")
                .HasColumnName("is_banned");
            entity.Property(e => e.IsArchive)
                .HasDefaultValueSql("false")
                .HasColumnName("is_archive");
            entity.Property(e => e.RoleId).HasColumnName("role_pk");

            entity.Property(e => e.InviteStatus)
                .HasColumnName("invite_status")
                .HasConversion<string>();

            entity.HasOne(d => d.AccountIdNavigation).WithMany(p => p.EventParticipants)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("event_participant_account_pk_fkey");

            entity.HasOne(d => d.EventIdNavigation).WithMany(p => p.EventParticipants)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("event_participant_event_pk_fkey");

            entity.HasOne(d => d.RoleIdNavigation).WithMany(p => p.EventParticipants)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("event_participant_role_pk_fkey");
        });

        modelBuilder.Entity<FeedbackModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("feedback_pkey");

            entity.ToTable("feedback");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.EventParticipantId).HasColumnName("event_participant_pk");
            entity.Property(e => e.Rating)
                .HasPrecision(1)
                .HasColumnName("rating");
            entity.Property(e => e.Text).HasColumnName("text");

            entity.HasOne(d => d.EventParticipantIdNavigation).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.EventParticipantId)
                .HasConstraintName("feedback_event_participant_pk_fkey");
        });

        modelBuilder.Entity<PermissionModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("permission_pkey");

            entity.ToTable("permission");

            entity.HasIndex(e => e.Name, "permission_name_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<RoleModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role");

            entity.HasIndex(e => e.Name, "role_name_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<RolePermissionModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_permission_pkey");

            entity.ToTable("role_permission");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.PermissionId).HasColumnName("permission_pk");
            entity.Property(e => e.RoleId).HasColumnName("role_pk");

            entity.HasOne(d => d.PermissionIdNavigation)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("role_permission_permission_pk_fkey");

            entity.HasOne(d => d.RoleIdNavigation)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("role_permission_role_pk_fkey");
        });
    }
}
