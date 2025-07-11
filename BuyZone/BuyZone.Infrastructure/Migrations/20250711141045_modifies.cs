﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuyZone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modifies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Logs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TypeOfAttack",
                table: "Logs",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "TypeOfAttack",
                table: "Logs");
        }
    }
}
