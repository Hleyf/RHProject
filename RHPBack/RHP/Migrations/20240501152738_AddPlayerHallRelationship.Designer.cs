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
    [Migration("20240501152738_AddPlayerHallRelationship")]
    partial class AddPlayerHallRelationship
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("action")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("HallId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("HallId");

                    b.HasIndex("PlayerId");

                    b.ToTable("ActionLog");
                });

            modelBuilder.Entity("RHP.Entities.Models.Dice", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("RollId")
                        .HasColumnType("int");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.Property<int>("value")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("RollId");

                    b.ToTable("Dice");
                });

            modelBuilder.Entity("RHP.Entities.Models.hall", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("hall");
                });

            modelBuilder.Entity("RHP.Entities.Models.player", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("name")
                        .IsUnique();

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("player");
                });

            modelBuilder.Entity("RHP.Entities.Models.Roll", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("HallId")
                        .HasColumnType("int");

                    b.Property<int>("modifier")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<int>("value")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("HallId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Roll");
                });

            modelBuilder.Entity("RHP.Entities.Models.user", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("role")
                        .HasColumnType("int");

                    b.Property<bool>("active")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("HallPlayer", b =>
                {
                    b.HasOne("RHP.Entities.Models.hall", null)
                        .WithMany()
                        .HasForeignKey("HallsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RHP.Entities.Models.player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RHP.Entities.Models.ActionLog", b =>
                {
                    b.HasOne("RHP.Entities.Models.hall", "hall")
                        .WithMany()
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RHP.Entities.Models.player", "player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hall");

                    b.Navigation("player");
                });

            modelBuilder.Entity("RHP.Entities.Models.Dice", b =>
                {
                    b.HasOne("RHP.Entities.Models.Roll", null)
                        .WithMany("dices")
                        .HasForeignKey("RollId");
                });

            modelBuilder.Entity("RHP.Entities.Models.hall", b =>
                {
                    b.HasOne("RHP.Entities.Models.player", "gameMaster")
                        .WithMany()
                        .HasForeignKey("id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("gameMaster");
                });

            modelBuilder.Entity("RHP.Entities.Models.player", b =>
                {
                    b.HasOne("RHP.Entities.Models.user", "user")
                        .WithOne("player")
                        .HasForeignKey("RHP.Entities.Models.player", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("RHP.Entities.Models.Roll", b =>
                {
                    b.HasOne("RHP.Entities.Models.hall", "hall")
                        .WithMany("rolls")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RHP.Entities.Models.player", "player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("hall");

                    b.Navigation("player");
                });

            modelBuilder.Entity("RHP.Entities.Models.hall", b =>
                {
                    b.Navigation("rolls");
                });

            modelBuilder.Entity("RHP.Entities.Models.Roll", b =>
                {
                    b.Navigation("dices");
                });

            modelBuilder.Entity("RHP.Entities.Models.user", b =>
                {
                    b.Navigation("player");
                });
#pragma warning restore 612, 618
        }
    }
}
