﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RHP.Data;

#nullable disable

namespace RHP.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240425143533_afterinitialchanges")]
    partial class afterinitialchanges
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RHP.Entities.Models.ActionLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("HallId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HallId");

                    b.HasIndex("PlayerId");

                    b.ToTable("ActionLog");
                });

            modelBuilder.Entity("RHP.Entities.Models.Dice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("RollId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RollId");

                    b.ToTable("Dice");
                });

            modelBuilder.Entity("RHP.Entities.Models.Hall", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Hall");
                });

            modelBuilder.Entity("RHP.Entities.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("HallId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HallId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Player");
                });

            modelBuilder.Entity("RHP.Entities.Models.Roll", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("HallId")
                        .HasColumnType("int");

                    b.Property<int>("Modifier")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HallId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Roll");
                });

            modelBuilder.Entity("RHP.Entities.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("E]Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<bool>("active")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("RHP.Entities.Models.ActionLog", b =>
                {
                    b.HasOne("RHP.Entities.Models.Hall", "Hall")
                        .WithMany()
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RHP.Entities.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hall");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("RHP.Entities.Models.Dice", b =>
                {
                    b.HasOne("RHP.Entities.Models.Roll", null)
                        .WithMany("Dices")
                        .HasForeignKey("RollId");
                });

            modelBuilder.Entity("RHP.Entities.Models.Hall", b =>
                {
                    b.HasOne("RHP.Entities.Models.Player", "GameMaster")
                        .WithMany("Halls")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameMaster");
                });

            modelBuilder.Entity("RHP.Entities.Models.Player", b =>
                {
                    b.HasOne("RHP.Entities.Models.Hall", null)
                        .WithMany("Players")
                        .HasForeignKey("HallId");

                    b.HasOne("RHP.Entities.Models.User", "User")
                        .WithOne("Player")
                        .HasForeignKey("RHP.Entities.Models.Player", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RHP.Entities.Models.Roll", b =>
                {
                    b.HasOne("RHP.Entities.Models.Hall", "Hall")
                        .WithMany("Rolls")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RHP.Entities.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hall");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("RHP.Entities.Models.Hall", b =>
                {
                    b.Navigation("Players");

                    b.Navigation("Rolls");
                });

            modelBuilder.Entity("RHP.Entities.Models.Player", b =>
                {
                    b.Navigation("Halls");
                });

            modelBuilder.Entity("RHP.Entities.Models.Roll", b =>
                {
                    b.Navigation("Dices");
                });

            modelBuilder.Entity("RHP.Entities.Models.User", b =>
                {
                    b.Navigation("Player");
                });
#pragma warning restore 612, 618
        }
    }
}
