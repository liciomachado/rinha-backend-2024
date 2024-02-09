﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RinhaBackend2024.Data;

#nullable disable

namespace RinhaBackend2024.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240208024238_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RinhaBackend2024.API.Domain.Client", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("Balance")
                        .HasColumnType("bigint")
                        .HasColumnName("balance");

                    b.Property<long>("Limit")
                        .HasColumnType("bigint")
                        .HasColumnName("limit");

                    b.HasKey("Id");

                    b.ToTable("clients", "public");
                });

            modelBuilder.Entity("RinhaBackend2024.API.Domain.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("Realized")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("realized");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<long>("Value")
                        .HasColumnType("bigint")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("transaction", "public");
                });

            modelBuilder.Entity("RinhaBackend2024.API.Domain.Transaction", b =>
                {
                    b.HasOne("RinhaBackend2024.API.Domain.Client", null)
                        .WithMany("Transations")
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("RinhaBackend2024.API.Domain.Client", b =>
                {
                    b.Navigation("Transations");
                });
#pragma warning restore 612, 618
        }
    }
}