﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VirtueApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "entries",
                columns: table => new
                {
                    entry_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    title = table.Column<string>(maxLength: 30, nullable: false),
                    description = table.Column<string>(maxLength: 1000, nullable: false),
                    starred = table.Column<bool>(nullable: false),
                    created_at = table.Column<DateTimeOffset>(nullable: false),
                    last_edited = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entries", x => x.entry_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    username = table.Column<string>(nullable: true),
                    password_hash = table.Column<byte[]>(nullable: true),
                    password_salt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "virtues",
                columns: table => new
                {
                    virtue_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(maxLength: 24, nullable: false),
                    color = table.Column<string>(maxLength: 10, nullable: false),
                    description = table.Column<string>(maxLength: 256, nullable: false),
                    icon = table.Column<string>(maxLength: 100, nullable: false),
                    created_at = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_virtues", x => x.virtue_id);
                });

            migrationBuilder.CreateTable(
                name: "virtue_entries",
                columns: table => new
                {
                    virtue_id = table.Column<int>(nullable: false),
                    entry_id = table.Column<int>(nullable: false),
                    difficulty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_virtue_entries", x => new { x.virtue_id, x.entry_id });
                    table.ForeignKey(
                        name: "fk_virtue_entries_entries_entry_id",
                        column: x => x.entry_id,
                        principalTable: "entries",
                        principalColumn: "entry_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_virtue_entries_virtues_virtue_id",
                        column: x => x.virtue_id,
                        principalTable: "virtues",
                        principalColumn: "virtue_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "virtues",
                columns: new[] { "virtue_id", "color", "created_at", "description", "icon", "name" },
                values: new object[,]
                {
                    { 1, "Red", new DateTimeOffset(new DateTime(2019, 7, 26, 17, 33, 7, 846, DateTimeKind.Unspecified).AddTicks(7970), new TimeSpan(0, -6, 0, 0, 0)), "Courageous Virtue", "Cool Icon", "Courage" },
                    { 2, "Blue", new DateTimeOffset(new DateTime(2019, 7, 26, 17, 33, 7, 849, DateTimeKind.Unspecified).AddTicks(1610), new TimeSpan(0, -6, 0, 0, 0)), "Sincere Virtue", "Cool Icon", "Sincerity" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_virtue_entries_entry_id",
                table: "virtue_entries",
                column: "entry_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "virtue_entries");

            migrationBuilder.DropTable(
                name: "entries");

            migrationBuilder.DropTable(
                name: "virtues");
        }
    }
}