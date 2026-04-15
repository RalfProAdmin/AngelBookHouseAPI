using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EkartAPI.Migrations
{
    public partial class Deliver_Address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_DeliveryAddress",
                columns: table => new
                {
                    AddId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlternateMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PinCode = table.Column<int>(type: "int", nullable: false),
                    HouseNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Landmark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DeliveryAddress", x => x.AddId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_DeliveryAddress");
        }
    }
}
