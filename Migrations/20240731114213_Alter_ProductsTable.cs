using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EkartAPI.Migrations
{
    public partial class Alter_ProductsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Benefits",
                table: "tbl_products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Benefits",
                table: "tbl_products");
        }
    }
}
