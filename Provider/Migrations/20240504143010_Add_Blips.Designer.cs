﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Provider;

namespace Provider.Migrations
{
    [DbContext(typeof(RageDbContext))]
    [Migration("20240504143010_Add_Blips")]
    partial class Add_Blips
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("DbModels.Blips", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double[]>("Position")
                        .HasColumnType("double precision[]");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Blips");
                });

            modelBuilder.Entity("DbModels.LoginModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("LoginModels");
                });

            modelBuilder.Entity("DbModels.TypesBlips", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("TypesBlips");
                });

            modelBuilder.Entity("DbModels.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<int>("LoginModelId")
                        .HasColumnType("integer");

                    b.Property<double>("Money")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<double[]>("OldPosition")
                        .HasColumnType("double precision[]");

                    b.HasKey("Id");

                    b.HasIndex("LoginModelId")
                        .IsUnique();

                    b.ToTable("UserModels");
                });

            modelBuilder.Entity("DbModels.VehicleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ColorOne")
                        .HasColumnType("integer");

                    b.Property<int>("ColorTwo")
                        .HasColumnType("integer");

                    b.Property<int>("Model")
                        .HasColumnType("integer");

                    b.Property<string>("Number")
                        .HasColumnType("character varying(5)")
                        .HasMaxLength(5);

                    b.Property<float[]>("OldPosition")
                        .HasColumnType("real[]");

                    b.Property<int>("OwnerID")
                        .HasColumnType("integer");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<float>("Rotation")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("OwnerID");

                    b.ToTable("VehicleModels");
                });

            modelBuilder.Entity("DbModels.VehiclePrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Model")
                        .HasColumnType("integer");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("VehiclePrice");
                });

            modelBuilder.Entity("DbModels.Blips", b =>
                {
                    b.HasOne("DbModels.TypesBlips", "TypeBlip")
                        .WithMany("Blips")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DbModels.UserModel", b =>
                {
                    b.HasOne("DbModels.LoginModel", "LoginModel")
                        .WithOne("UserModel")
                        .HasForeignKey("DbModels.UserModel", "LoginModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DbModels.VehicleModel", b =>
                {
                    b.HasOne("DbModels.UserModel", "UserModel")
                        .WithMany("Vehicles")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}