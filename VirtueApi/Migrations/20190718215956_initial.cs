using System;
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
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    last_edited = table.Column<DateTime>(nullable: true),
                    starred = table.Column<bool>(nullable: false)
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
                    created_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_virtues", x => x.virtue_id);
                });

            migrationBuilder.CreateTable(
                name: "virtue_entry",
                columns: table => new
                {
                    virtue_id = table.Column<int>(nullable: false),
                    entry_id = table.Column<int>(nullable: false),
                    difficulty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_virtue_entry", x => new { x.virtue_id, x.entry_id });
                    table.ForeignKey(
                        name: "fk_virtue_entry_entries_entry_id",
                        column: x => x.entry_id,
                        principalTable: "entries",
                        principalColumn: "entry_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_virtue_entry_virtues_virtue_id",
                        column: x => x.virtue_id,
                        principalTable: "virtues",
                        principalColumn: "virtue_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "entries",
                columns: new[] { "entry_id", "created_at", "description", "last_edited", "starred", "title" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blah blah blah", null, true, "My first entry" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blah blah blah", null, true, "My second entry" }
                });

            migrationBuilder.InsertData(
                table: "virtues",
                columns: new[] { "virtue_id", "color", "created_at", "description", "icon", "name" },
                values: new object[,]
                {
                    { 1, "Red", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Courageous Virtue", "Cool Icon", "Courage" },
                    { 2, "Blue", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sincere Virtue", "Cool Icon", "Sincerity" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_virtue_entry_entry_id",
                table: "virtue_entry",
                column: "entry_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "virtue_entry");

            migrationBuilder.DropTable(
                name: "entries");

            migrationBuilder.DropTable(
                name: "virtues");
        }
    }
}
