using System;

namespace Hotel.Booking.Application.DTOs;

public class BookingResponseDTO
{
    public Guid BookingId { get; set; }
    public string RazorpayOrderId { get; set; }=null!;
    public decimal Amount { get; set; }
    public string Key { get; set; }=null!;
}
// return new BookingResponse
//         {
//             BookingId = booking.Id,
//             RazorpayOrderId = razorpayOrderId,
//             Amount = amount,
//             Key = "YOUR_PUBLIC_KEY_ID" // Send public key to frontend
//         };