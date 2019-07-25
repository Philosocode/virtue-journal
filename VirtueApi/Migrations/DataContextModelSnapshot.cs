﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VirtueApi.Data;

namespace VirtueApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("VirtueApi.Data.Entities.Entry", b =>
                {
                    b.Property<int>("EntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("entry_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("LastEdited")
                        .HasColumnName("last_edited");

                    b.Property<bool>("Starred")
                        .HasColumnName("starred");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("title")
                        .HasMaxLength(30);

                    b.HasKey("EntryId")
                        .HasName("pk_entries");

                    b.ToTable("entries");
                });

            modelBuilder.Entity("VirtueApi.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnName("password_hash");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnName("password_salt");

                    b.Property<string>("Username")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users");
                });

            modelBuilder.Entity("VirtueApi.Data.Entities.Virtue", b =>
                {
                    b.Property<int>("VirtueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("virtue_id");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnName("color")
                        .HasMaxLength(10);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasMaxLength(256);

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnName("icon")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasMaxLength(24);

                    b.HasKey("VirtueId")
                        .HasName("pk_virtues");

                    b.ToTable("virtues");

                    b.HasData(
                        new
                        {
                            VirtueId = 1,
                            Color = "Red",
                            CreatedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Courageous Virtue",
                            Icon = "Cool Icon",
                            Name = "Courage"
                        });
                });

            modelBuilder.Entity("VirtueApi.Data.Entities.VirtueEntry", b =>
                {
                    b.Property<int>("VirtueId")
                        .HasColumnName("virtue_id");

                    b.Property<int>("EntryId")
                        .HasColumnName("entry_id");

                    b.Property<int>("Difficulty")
                        .HasColumnName("difficulty");

                    b.HasKey("VirtueId", "EntryId");

                    b.HasIndex("EntryId")
                        .HasName("ix_virtue_entries_entry_id");

                    b.ToTable("virtue_entries");
                });

            modelBuilder.Entity("VirtueApi.Data.Entities.VirtueEntry", b =>
                {
                    b.HasOne("VirtueApi.Data.Entities.Entry", "Entry")
                        .WithMany("VirtuesLink")
                        .HasForeignKey("EntryId")
                        .HasConstraintName("fk_virtue_entries_entries_entry_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("VirtueApi.Data.Entities.Virtue", "Virtue")
                        .WithMany("EntriesLink")
                        .HasForeignKey("VirtueId")
                        .HasConstraintName("fk_virtue_entries_virtues_virtue_id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
