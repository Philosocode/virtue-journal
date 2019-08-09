using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VirtueJournal.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    user_name = table.Column<string>(nullable: true),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    password_hash = table.Column<byte[]>(nullable: true),
                    password_salt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.user_id);
                });

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
                    last_edited = table.Column<DateTimeOffset>(nullable: true),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_entries", x => x.entry_id);
                    table.ForeignKey(
                        name: "fk_entries_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
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
                    created_at = table.Column<DateTimeOffset>(nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_virtues", x => x.virtue_id);
                    table.ForeignKey(
                        name: "fk_virtues_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.PrimaryKey("pk_virtue_entries", x => new { x.virtue_id, x.entry_id });
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

            migrationBuilder.CreateIndex(
                name: "ix_entries_user_id",
                table: "entries",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_virtue_entries_entry_id",
                table: "virtue_entries",
                column: "entry_id");

            migrationBuilder.CreateIndex(
                name: "ix_virtues_user_id",
                table: "virtues",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "virtue_entries");

            migrationBuilder.DropTable(
                name: "entries");

            migrationBuilder.DropTable(
                name: "virtues");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
