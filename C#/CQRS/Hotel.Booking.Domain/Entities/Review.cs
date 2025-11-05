using System;

namespace Hotel.Booking.Domain.Entities;

public class Review
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid HotelId { get; private set; }
    public int Rating { get; private set; }
    public string Comment { get; private set; } = null!;
    public DateTime ReviewDate { get; private set; }

    public Customer Customer { get; private set; } = null!;
    public Hotel Hotel { get; private set; } = null!;

    public static Review Create(Guid customerId, Guid hotelId, int rating, string comment)
    {
        return new Review
        {
            CustomerId = customerId,
            HotelId = hotelId,
            Rating = rating,
            Comment = comment,
            ReviewDate = DateTime.UtcNow
        };
    }

    public void UpdateDetails(int? rating, string? comment)
    {
        if (rating.HasValue)
        {
            Rating = rating.Value;
        }
        if (!string.IsNullOrWhiteSpace(comment))
        {
            Comment = comment;
        }
    }
}
