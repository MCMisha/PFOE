using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApi.Models;

namespace WebApi.Contexts
{
    public partial class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<FailedLogin> FailedLogins { get; set; } = null!;
        public virtual DbSet<Participant> Participants { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("AppDB"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("event");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .HasColumnName("category");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("creation_date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Date).HasColumnName("date");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .HasColumnName("location");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Organizer).HasColumnName("organizer");

                entity.Property(e => e.ParticipantNumber).HasColumnName("participant_number");

                entity.Property(e => e.VisitsNumber).HasColumnName("visits_number");
            });

            modelBuilder.Entity<FailedLogin>(entity =>
            {
                entity.ToTable("failed_login");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FailedLoginAttempts)
                    .HasColumnName("failed_login_attempts")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.LastLoginTime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("last_login_time");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.ToTable("participant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("setting");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FontSize).HasColumnName("font_size");

                entity.Property(e => e.Style)
                    .HasMaxLength(50)
                    .HasColumnName("style");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(30)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(40)
                    .HasColumnName("last_name");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .HasMaxLength(64)
                    .HasColumnName("password");
            });

            modelBuilder.HasSequence<int>("event_id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
