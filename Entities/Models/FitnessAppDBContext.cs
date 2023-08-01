using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities.Models
{
    public partial class FitnessAppDBContext : DbContext
    {
        public FitnessAppDBContext()
        {
        }

        public FitnessAppDBContext(DbContextOptions<FitnessAppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Exercise> Exercises { get; set; } = null!;
        public virtual DbSet<Measurement> Measurements { get; set; } = null!;
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public virtual DbSet<Routine> Routines { get; set; } = null!;
        public virtual DbSet<RoutineExercise> RoutineExercises { get; set; } = null!;
        public virtual DbSet<Set> Sets { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Workout> Workouts { get; set; } = null!;
        public virtual DbSet<WorkoutExercise> WorkoutExercises { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-U3280M2;Database=FitnessAppDB;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.ToTable("Exercise");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Measurement>(entity =>
            {
                entity.ToTable("Measurement");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Measurements)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Measurement_User");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.IssuedDate).HasColumnType("datetime");

                entity.Property(e => e.Token).HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__RefreshTo__UserI__49C3F6B7");
            });

            modelBuilder.Entity<Routine>(entity =>
            {
                entity.ToTable("Routine");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Routines)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Routine_User");
            });

            modelBuilder.Entity<RoutineExercise>(entity =>
            {
                entity.HasKey(e => new { e.RoutineId, e.ExerciseId })
                    .HasName("RoutineExercise_pk");

                entity.ToTable("RoutineExercise");

                entity.Property(e => e.RoutineId).HasColumnName("Routine_Id");

                entity.Property(e => e.ExerciseId).HasColumnName("Exercise_Id");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.RoutineExercises)
                    .HasForeignKey(d => d.ExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RoutineExercise_Exercise");

                entity.HasOne(d => d.Routine)
                    .WithMany(p => p.RoutineExercises)
                    .HasForeignKey(d => d.RoutineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RoutineExercise_Routine");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.ToTable("Set");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.WorkoutExerciseId).HasColumnName("WorkoutExercise_Id");

                entity.HasOne(d => d.WorkoutExercise)
                    .WithMany(p => p.Sets)
                    .HasForeignKey(d => d.WorkoutExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Set_WorkoutExercise");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(100);
            });

            modelBuilder.Entity<Workout>(entity =>
            {
                entity.ToTable("Workout");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TimeEnded).HasColumnType("datetime");

                entity.Property(e => e.TimeStarted).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Workouts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Workout_User");
            });

            modelBuilder.Entity<WorkoutExercise>(entity =>
            {
                entity.ToTable("WorkoutExercise");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ExerciseId).HasColumnName("Exercise_Id");

                entity.Property(e => e.WorkoutId).HasColumnName("Workout_Id");

                entity.HasOne(d => d.Exercise)
                    .WithMany(p => p.WorkoutExercises)
                    .HasForeignKey(d => d.ExerciseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("WorkoutExercise_Exercise");

                entity.HasOne(d => d.Workout)
                    .WithMany(p => p.WorkoutExercises)
                    .HasForeignKey(d => d.WorkoutId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("WorkoutExercise_Workout");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
