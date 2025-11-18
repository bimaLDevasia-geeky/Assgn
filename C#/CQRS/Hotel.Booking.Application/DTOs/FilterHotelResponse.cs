using System;

namespace Hotel.Booking.Application.DTOs;

public class FilterHotelResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public double MinPrice { get; set; }
    public double AverageRating { get; set; }
    public string Country { get; set; } = string.Empty;
    public int? StarRating { get; set; }
}
