using System;

namespace Hotel.Booking.Domain.Entities;

public enum BookingStatus
{
    Pending,
    Confirmed,
    Cancelled,
    Completed
}
public class Booking
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid RoomId { get; private set; }
    public DateTime CheckInDate { get; private set; }
    public DateTime CheckOutDate { get; private set; }
    public BookingStatus Status { get; private set; }
    public decimal TotalAmount { get; private set; }

    public Customer Customer { get; private set; } = null!;
    public Room Room { get; private set; } = null!;
    public Payment Payment { get; private set; } = null!;

    public static Booking Create(Guid customerId, Guid roomId, DateTime checkInDate, DateTime checkOutDate, decimal totalAmount)
    {
        return new Booking
        {
            CustomerId = customerId,
            RoomId = roomId,
            CheckInDate = checkInDate,
            CheckOutDate = checkOutDate,
            TotalAmount = totalAmount,
            Status = BookingStatus.Pending
        };
    }


    public void UpdateBookingDetails(BookingStatus? status, decimal? totalAmount,  Guid? roomId, DateTime? checkInDate, DateTime? checkOutDate)
    {
        if (status.HasValue)
        {
            Status = status.Value;
        }
        if (totalAmount.HasValue)
        {
            TotalAmount = totalAmount.Value;
        }
        
        if (roomId.HasValue)
        {
            RoomId = roomId.Value;
        }
        if (checkInDate.HasValue)
        {
            CheckInDate = checkInDate.Value;
        }
        if (checkOutDate.HasValue)
        {
            CheckOutDate = checkOutDate.Value;
        }
    }
}
