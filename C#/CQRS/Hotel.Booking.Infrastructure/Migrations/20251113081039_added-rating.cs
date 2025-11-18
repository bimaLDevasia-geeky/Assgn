using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedrating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StarRating",
                table: "Hotels",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarRating",
                table: "Hotels");
        }
    }
}
