using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GymAppAPI.Models;

public partial class GymAppDbContext : DbContext
{
    public GymAppDbContext()
    {
    }

    public GymAppDbContext(DbContextOptions<GymAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<MembershipPrice> MembershipPrices { get; set; }

    public virtual DbSet<MembershipStatus> MembershipStatuses { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=AORUSPRO;Database=GymAppDB;Trusted_Connection=True; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.IdAdmin);

            entity.ToTable("Admin");

            entity.Property(e => e.IdAdmin).HasColumnName("id_admin");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.IdAttendance);

            entity.ToTable("Attendance");

            entity.Property(e => e.IdAttendance).HasColumnName("id_attendance");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.FinishingTime)
                .HasColumnType("date")
                .HasColumnName("finishingTime");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.StartingTime)
                .HasColumnType("date")
                .HasColumnName("startingTime");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attendance_Client");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient);

            entity.ToTable("Client");

            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("birthDate");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.MembershipType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("membershipType");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.RegistrationDate)
                .HasColumnType("date")
                .HasColumnName("registrationDate");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Client_Users");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.IdInstructors);

            entity.ToTable("Instructor");

            entity.Property(e => e.IdInstructors).HasColumnName("id_instructors");
            entity.Property(e => e.LastName)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("lastName");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Speciality)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("speciality");
        });

        modelBuilder.Entity<MembershipPrice>(entity =>
        {
            entity.HasKey(e => e.IdMembership).HasName("PK_Membership");

            entity.ToTable("MembershipPrice");

            entity.Property(e => e.IdMembership).HasColumnName("id_membership");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.MembershipType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("membershipType");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(15, 0)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<MembershipStatus>(entity =>
        {
            entity.HasKey(e => e.IdPayment).HasName("PK_Payments");

            entity.ToTable("MembershipStatus");

            entity.Property(e => e.IdPayment).HasColumnName("id_payment");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("date")
                .HasColumnName("expirationDate");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("date")
                .HasColumnName("paymentDate");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.MembershipStatuses)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MembershipStatus_Client");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.IdPayment).HasName("PK_Payments_1");

            entity.Property(e => e.IdPayment).HasColumnName("id_payment");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("amount");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("date")
                .HasColumnName("paymentDate");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Payments)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Client");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.NickName)
                .HasMaxLength(25)
                .IsFixedLength()
                .HasColumnName("nickName");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
