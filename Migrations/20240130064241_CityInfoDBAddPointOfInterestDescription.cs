﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.api.Migrations
{
    /// <inheritdoc />
    public partial class CityInfoDBAddPointOfInterestDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PointOfInrest",
                type: "TEXT",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PointOfInrest");
        }
    }
}
