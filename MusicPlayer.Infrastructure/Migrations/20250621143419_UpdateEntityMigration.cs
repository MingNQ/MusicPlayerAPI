using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicPlayer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tracks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Tracks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Artists",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Artists",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Albums",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Albums");
        }
    }
}
