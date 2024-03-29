﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MinimalScheduleAPI.Persistence;

#nullable disable

namespace MinimalScheduleAPI.Persistence.Migrations
{
    [DbContext(typeof(CardDbContext))]
    [Migration("20240129000507_firstMigration")]
    partial class firstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MinimalScheduleAPI.Model.Card", b =>
                {
                    b.Property<Guid>("IdCard")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("BDiaTodo")
                        .HasColumnType("bit");

                    b.Property<string>("SDescricao")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("SLocal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCard");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("MinimalScheduleAPI.Model.ToDo", b =>
                {
                    b.Property<Guid>("IdToDo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("BConcluido")
                        .HasColumnType("bit");

                    b.Property<Guid>("IdCard")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SDescricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("STitulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdToDo");

                    b.HasIndex("IdCard");

                    b.ToTable("ToDos");
                });

            modelBuilder.Entity("MinimalScheduleAPI.Model.ToDo", b =>
                {
                    b.HasOne("MinimalScheduleAPI.Model.Card", null)
                        .WithMany("ListToDo")
                        .HasForeignKey("IdCard");
                });

            modelBuilder.Entity("MinimalScheduleAPI.Model.Card", b =>
                {
                    b.Navigation("ListToDo");
                });
#pragma warning restore 612, 618
        }
    }
}
