﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeInk.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsPublishedColumnToBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Books");
        }
    }
}
