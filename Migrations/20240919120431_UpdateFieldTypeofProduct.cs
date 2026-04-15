using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EkartAPI.Migrations
{
    public partial class UpdateFieldTypeofProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the existing 'Category' column
            migrationBuilder.DropColumn(
                name: "Category",
                table: "tbl_products");

            // Add the new 'CategoryId' column
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "tbl_products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert changes: add back the 'Category' column and drop the 'CategoryId' column
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "tbl_products");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "tbl_products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }

}
