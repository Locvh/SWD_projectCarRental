﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace API.Model
{
    public partial class CarRentalContext : DbContext
    {
        public CarRentalContext()
        {
        }

        public CarRentalContext(DbContextOptions<CarRentalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<BookingDetail> BookingDetails { get; set; }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Garage> Garages { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=CarRental;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.AccountId)
                    .HasMaxLength(10)
                    .HasColumnName("AccountID");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(10);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.ResId);

                entity.ToTable("Booking");

                entity.Property(e => e.ResId)
                    .ValueGeneratedNever()
                    .HasColumnName("ResID");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(10)
                    .HasColumnName("AccountID");

                entity.Property(e => e.PickUpDate).HasColumnType("datetime");

                entity.Property(e => e.PickUpLocation).HasMaxLength(50);

                entity.Property(e => e.ReturnDate).HasColumnType("datetime");

                entity.Property(e => e.ReturnLocation).HasMaxLength(50);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Booking_Accounts");
            });

            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.HasKey(e => new { e.ResId, e.CarId });

                entity.ToTable("BookingDetail");

                entity.Property(e => e.ResId).HasColumnName("ResID");

                entity.Property(e => e.CarId).HasColumnName("CarID");

                entity.Property(e => e.FormDate).HasColumnType("datetime");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingDetail_Car");

                entity.HasOne(d => d.Res)
                    .WithMany(p => p.BookingDetails)
                    .HasForeignKey(d => d.ResId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BookingDetail_Booking");
            });

            modelBuilder.Entity<Car>(entity =>
            {
                entity.ToTable("Car");

                entity.Property(e => e.CarId)
                    .ValueGeneratedNever()
                    .HasColumnName("CarID");

                entity.Property(e => e.Brand).HasMaxLength(50);

                entity.Property(e => e.CateId).HasColumnName("CateID");

                entity.Property(e => e.DescriptionCar).HasMaxLength(50);

                entity.Property(e => e.GarageId).HasColumnName("GarageID");

                entity.Property(e => e.ImageLink).HasMaxLength(200);

                entity.Property(e => e.LicensePlates).HasMaxLength(50);

                entity.Property(e => e.VehicleFare).HasMaxLength(50);

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.CateId)
                    .HasConstraintName("FK_Car_Category");

                entity.HasOne(d => d.Garage)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.GarageId)
                    .HasConstraintName("FK_Car_Garage");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CateId);

                entity.ToTable("Category");

                entity.Property(e => e.CateId)
                    .ValueGeneratedNever()
                    .HasColumnName("CateID");

                entity.Property(e => e.Colour).HasMaxLength(50);

                entity.Property(e => e.Manufacture).HasMaxLength(50);

                entity.Property(e => e.Model).HasMaxLength(50);
            });

            modelBuilder.Entity<Garage>(entity =>
            {
                entity.ToTable("Garage");

                entity.Property(e => e.GarageId)
                    .ValueGeneratedNever()
                    .HasColumnName("GarageID");

                entity.Property(e => e.Address).HasMaxLength(70);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.ImageLink).HasMaxLength(200);
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.ToTable("Profile");

                entity.Property(e => e.ProfileId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProfileID");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(10)
                    .HasColumnName("AccountID");

                entity.Property(e => e.Address).HasMaxLength(70);

                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.IdentityCard)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Phone).HasMaxLength(15);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Profiles)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Profile_Accounts");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
