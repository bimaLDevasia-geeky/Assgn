using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel.Booking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class idpay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Payments",
                newName: "PaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Payments",
                newName: "TransactionId");
        }
    }
}
