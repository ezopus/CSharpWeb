﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeminarHub.Migrations
{
    public partial class SeminarModelBoolDeleteAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Seminars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Seminars");
        }
    }
}
