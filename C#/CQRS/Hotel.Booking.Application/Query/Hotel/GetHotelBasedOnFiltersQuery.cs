using System;
using Hotel.Booking.Application.DTOs;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
namespace Hotel.Booking.Application.Query.Hotel;

public class GetHotelBasedOnFiltersQuery:IRequest<List<FilterHotelResponse>>
{
    public string? Destination { get; set; }
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int[]? StarRating { get; set; }

    public string? SortBy { get; set; }  
}
