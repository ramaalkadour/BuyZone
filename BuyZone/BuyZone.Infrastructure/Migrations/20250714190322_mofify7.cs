using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuyZone.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mofify7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Employee_Status",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Employee_Status",
                table: "AspNetUsers");
        }
    }
}
