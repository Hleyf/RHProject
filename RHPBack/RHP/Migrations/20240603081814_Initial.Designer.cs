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
    [Migration("20240603081814_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ContactUser", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ContactsRequestorId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ContactsRecipientId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "ContactsRequestorId", "ContactsRecipientId");

                    b.HasIndex("ContactsRequestorId", "ContactsRecipientId");

                    b.ToTable("ContactUser");
                });

            modelBuilder.Entity("HallPlayer", b =>
                {
                    b.Property<int>("HallsId")
                        .HasColumnType("int");

                    b.Property<int>("PlayersId")
                        .HasColumnType("int");

                    b.HasKey("HallsId", "PlayersId");

                    b.HasIndex("PlayersId");

                    b.ToTable("HallPlayer");
                });

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

                    b.ToTable("ActionLogs");
                });

            modelBuilder.Entity("RHP.Entities.Models.Contact", b =>
                {
                    b.Property<string>("RequestorId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RecipientId")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("RequestorId", "RecipientId");

                    b.HasIndex("RecipientId");

                    b.ToTable("Contacts");
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

                    b.ToTable("Dices");
                });

            modelBuilder.Entity("RHP.Entities.Models.Hall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("GMId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("GMId");

                    b.ToTable("Halls");
                });

            modelBuilder.Entity("RHP.Entities.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Players");
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

                    b.ToTable("Rolls");
                });

            modelBuilder.Entity("RHP.Entities.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("active")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("lastLogin")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("loggedIn")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ContactUser", b =>
                {
                    b.HasOne("RHP.Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RHP.Entities.Models.Contact", null)
                        .WithMany()
                        .HasForeignKey("ContactsRequestorId", "ContactsRecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HallPlayer", b =>
                {
                    b.HasOne("RHP.Entities.Models.Hall", null)
                        .WithMany()
                        .HasForeignKey("HallsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RHP.Entities.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("RHP.Entities.Models.Contact", b =>
                {
                    b.HasOne("RHP.Entities.Models.User", "Recipient")
                        .WithMany()
                        .HasForeignKey("RecipientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RHP.Entities.Models.User", "Requestor")
                        .WithMany()
                        .HasForeignKey("RequestorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipient");

                    b.Navigation("Requestor");
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
                        .WithMany()
                        .HasForeignKey("GMId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_GM");

                    b.Navigation("GameMaster");
                });

            modelBuilder.Entity("RHP.Entities.Models.Player", b =>
                {
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
                    b.Navigation("Rolls");
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