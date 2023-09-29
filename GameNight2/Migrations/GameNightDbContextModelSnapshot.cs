﻿// <auto-generated />
using System;
using GameNight2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameNight2.Migrations
{
    [DbContext(typeof(GameNightDbContext))]
    partial class GameNightDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GameNight2.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("GameNight2.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Is18Plus")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NightId")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NightId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("GameNight2.Models.Night", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("int");

                    b.Property<int>("OrganisatorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganisatorId");

                    b.ToTable("Nights");
                });

            modelBuilder.Entity("GameNight2.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Person", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("GameNight2.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NightId")
                        .HasColumnType("int");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WriterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NightId");

                    b.HasIndex("WriterId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("GameNight2.Models.Snack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NightId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NightId");

                    b.ToTable("Snacks");
                });

            modelBuilder.Entity("GameNight2.Models.Organisator", b =>
                {
                    b.HasBaseType("GameNight2.Models.Person");

                    b.HasDiscriminator().HasValue("Organisator");
                });

            modelBuilder.Entity("GameNight2.Models.Player", b =>
                {
                    b.HasBaseType("GameNight2.Models.Person");

                    b.Property<int?>("NightId")
                        .HasColumnType("int");

                    b.HasIndex("NightId");

                    b.HasDiscriminator().HasValue("Player");
                });

            modelBuilder.Entity("GameNight2.Models.Game", b =>
                {
                    b.HasOne("GameNight2.Models.Night", null)
                        .WithMany("Games")
                        .HasForeignKey("NightId");

                    b.HasOne("GameNight2.Models.Player", null)
                        .WithMany("Games")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("GameNight2.Models.Night", b =>
                {
                    b.HasOne("GameNight2.Models.Organisator", "Organisator")
                        .WithMany("Nights")
                        .HasForeignKey("OrganisatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organisator");
                });

            modelBuilder.Entity("GameNight2.Models.Person", b =>
                {
                    b.HasOne("GameNight2.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("GameNight2.Models.Review", b =>
                {
                    b.HasOne("GameNight2.Models.Night", null)
                        .WithMany("Reviews")
                        .HasForeignKey("NightId");

                    b.HasOne("GameNight2.Models.Person", "Writer")
                        .WithMany()
                        .HasForeignKey("WriterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Writer");
                });

            modelBuilder.Entity("GameNight2.Models.Snack", b =>
                {
                    b.HasOne("GameNight2.Models.Night", null)
                        .WithMany("Snacks")
                        .HasForeignKey("NightId");
                });

            modelBuilder.Entity("GameNight2.Models.Player", b =>
                {
                    b.HasOne("GameNight2.Models.Night", null)
                        .WithMany("Players")
                        .HasForeignKey("NightId");
                });

            modelBuilder.Entity("GameNight2.Models.Night", b =>
                {
                    b.Navigation("Games");

                    b.Navigation("Players");

                    b.Navigation("Reviews");

                    b.Navigation("Snacks");
                });

            modelBuilder.Entity("GameNight2.Models.Organisator", b =>
                {
                    b.Navigation("Nights");
                });

            modelBuilder.Entity("GameNight2.Models.Player", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
