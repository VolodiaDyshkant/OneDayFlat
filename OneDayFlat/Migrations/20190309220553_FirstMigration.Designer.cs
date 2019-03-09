﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OneDayFlat.Data;

namespace OneDayFlat.Migrations
{
    [DbContext(typeof(OneDayFlatContext))]
    [Migration("20190309220553_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OneDayFlat.Models.Calendar", b =>
                {
                    b.Property<int>("CalendarID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CurrentTime");

                    b.Property<int>("FlatID");

                    b.HasKey("CalendarID");

                    b.ToTable("Calendar");
                });

            modelBuilder.Entity("OneDayFlat.Models.Day", b =>
                {
                    b.Property<int>("DayID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Booked");

                    b.Property<int>("CalendarID");

                    b.Property<int?>("UserID");

                    b.HasKey("DayID");

                    b.HasIndex("CalendarID");

                    b.ToTable("Day");
                });

            modelBuilder.Entity("OneDayFlat.Models.Flat", b =>
                {
                    b.Property<int>("RoomID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CalendarID");

                    b.Property<string>("City");

                    b.Property<string>("Description");

                    b.Property<byte[]>("Image");

                    b.Property<string>("OwnerName");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("RoomID");

                    b.HasIndex("CalendarID")
                        .IsUnique();

                    b.ToTable("Flat");
                });

            modelBuilder.Entity("OneDayFlat.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("OneDayFlat.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DayForeignKey");

                    b.Property<string>("Login")
                        .IsRequired();

                    b.Property<string>("Name");

                    b.Property<string>("Number");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("RoleID");

                    b.HasKey("UserID");

                    b.HasIndex("DayForeignKey")
                        .IsUnique()
                        .HasFilter("[DayForeignKey] IS NOT NULL");

                    b.HasIndex("RoleID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("OneDayFlat.Models.UserFlat", b =>
                {
                    b.Property<int>("UserFlatID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FlatID");

                    b.Property<int>("UserID");

                    b.HasKey("UserFlatID");

                    b.HasIndex("FlatID");

                    b.HasIndex("UserID");

                    b.ToTable("UserFlat");
                });

            modelBuilder.Entity("OneDayFlat.Models.Day", b =>
                {
                    b.HasOne("OneDayFlat.Models.Calendar", "Calendar")
                        .WithMany("Days")
                        .HasForeignKey("CalendarID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OneDayFlat.Models.Flat", b =>
                {
                    b.HasOne("OneDayFlat.Models.Calendar", "Calendar")
                        .WithOne("Flat")
                        .HasForeignKey("OneDayFlat.Models.Flat", "CalendarID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OneDayFlat.Models.User", b =>
                {
                    b.HasOne("OneDayFlat.Models.Day", "Day")
                        .WithOne("User")
                        .HasForeignKey("OneDayFlat.Models.User", "DayForeignKey");

                    b.HasOne("OneDayFlat.Models.Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OneDayFlat.Models.UserFlat", b =>
                {
                    b.HasOne("OneDayFlat.Models.Flat", "Flat")
                        .WithMany("UserFlat")
                        .HasForeignKey("FlatID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OneDayFlat.Models.User", "User")
                        .WithMany("UserFlat")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}