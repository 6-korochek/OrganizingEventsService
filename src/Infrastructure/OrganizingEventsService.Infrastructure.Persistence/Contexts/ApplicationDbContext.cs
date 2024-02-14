using Microsoft.EntityFrameworkCore;
using OrganizingEventsService.Application.Models.Entities.Enums;
using OrganizingEventsService.Infrastructure.Persistence.Entities;

namespace OrganizingEventsService.Infrastructure.Persistence.Contexts;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventParticipant> EventParticipants { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolePermission> RolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum<EventStatus>()
            .HasPostgresEnum<EventParticipantInviteStatus>();

        modelBuilder.Entity<Account>(entity =>
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
            entity.Property(e => e.Email)
                .HasMaxLength(70)
                .HasColumnName("email");
            entity.Property(e => e.IsInvite)
                .HasDefaultValueSql("true")
                .HasColumnName("is_invite");
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

        modelBuilder.Entity<Event>(entity =>
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

        modelBuilder.Entity<EventParticipant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("event_participant_pkey");

            entity.ToTable("event_participant");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AccountPk).HasColumnName("account_pk");
            entity.Property(e => e.EventPk).HasColumnName("event_pk");
            entity.Property(e => e.IsBanned)
                .HasDefaultValueSql("false")
                .HasColumnName("is_banned");
            entity.Property(e => e.RolePk).HasColumnName("role_pk");

            entity.Property(e => e.InviteStatus)
                .HasColumnName("invite_status")
                .HasConversion<string>();

            entity.HasOne(d => d.AccountPkNavigation).WithMany(p => p.EventParticipants)
                .HasForeignKey(d => d.AccountPk)
                .HasConstraintName("event_participant_account_pk_fkey");

            entity.HasOne(d => d.EventPkNavigation).WithMany(p => p.EventParticipants)
                .HasForeignKey(d => d.EventPk)
                .HasConstraintName("event_participant_event_pk_fkey");

            entity.HasOne(d => d.RolePkNavigation).WithMany(p => p.EventParticipants)
                .HasForeignKey(d => d.RolePk)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("event_participant_role_pk_fkey");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("feedback_pkey");

            entity.ToTable("feedback");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.EventParticipantPk).HasColumnName("event_participant_pk");
            entity.Property(e => e.Rating)
                .HasPrecision(1)
                .HasColumnName("rating");
            entity.Property(e => e.Text).HasColumnName("text");

            entity.HasOne(d => d.EventParticipantPkNavigation).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.EventParticipantPk)
                .HasConstraintName("feedback_event_participant_pk_fkey");
        });

        modelBuilder.Entity<Permission>(entity =>
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

        modelBuilder.Entity<Role>(entity =>
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

        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_permission_pkey");

            entity.ToTable("role_permission");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.PermissionPk).HasColumnName("permission_pk");
            entity.Property(e => e.RolePk).HasColumnName("role_pk");

            entity.HasOne(d => d.PermissionPkNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.PermissionPk)
                .HasConstraintName("role_permission_permission_pk_fkey");

            entity.HasOne(d => d.RolePkNavigation).WithMany(p => p.RolePermissions)
                .HasForeignKey(d => d.RolePk)
                .HasConstraintName("role_permission_role_pk_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
