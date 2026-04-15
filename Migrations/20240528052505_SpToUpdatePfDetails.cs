using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EkartAPI.Migrations
{
    public partial class SpToUpdatePfDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE Sp_UpdateUserName
                @UserId INT,
                @FirstName NVARCHAR(50),
                @LastName NVARCHAR(50)
            AS
            BEGIN
                SET NOCOUNT ON;

                UPDATE [EkartDataB].[dbo].[tbl_users]
                SET [FirstName] = @FirstName,
                    [LastName] = @LastName,
                    [UpdatedAt] = GETUTCDATE()
                WHERE [UserId] = @UserId;
            END
            GO

        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE Sp_UpdateUserEmail
                @UserId INT,
                @Email NVARCHAR(100)
            AS
            BEGIN
                SET NOCOUNT ON;

                UPDATE [EkartDataB].[dbo].[tbl_users]
                SET [Email] = @Email,
                    [UpdatedAt] = GETUTCDATE()
                WHERE [UserId] = @UserId;
            END
            GO

        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE Sp_UpdateUserPhoneNumber
                @UserId INT,
                @MobileNumber NVARCHAR(20)
            AS
            BEGIN
                SET NOCOUNT ON;

                UPDATE [EkartDataB].[dbo].[tbl_users]
                SET [MobileNumber] = @MobileNumber,
                    [UpdatedAt] = GETUTCDATE()
                WHERE [UserId] = @UserId;
            END
            GO

        ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
