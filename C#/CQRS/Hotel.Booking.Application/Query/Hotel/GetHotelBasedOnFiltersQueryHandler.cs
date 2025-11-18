using System;
using System.Linq;
using Hotel.Booking.Application.DTOs;
using Hotel.Booking.Application.Interfaces;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Query.Hotel;

public class GetHotelBasedOnFiltersQueryHandler:IRequestHandler<GetHotelBasedOnFiltersQuery,List<FilterHotelResponse>>
{
    private readonly IHotelQueryService _hotelservice;
    public GetHotelBasedOnFiltersQueryHandler(IHotelQueryService hotelservice)
    {
        _hotelservice = hotelservice;
    }

    public async Task<List<FilterHotelResponse>> Handle(GetHotelBasedOnFiltersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<appDomain.Hotel> hotelsQuery = await _hotelservice.GetHotelsAsQuerable(cancellationToken);

        hotelsQuery = hotelsQuery.Where(h => h.Rooms.Where(r=>r.Status==appDomain.RoomStatus.Available).Any());
        if (request.Destination is not null)
        {
            hotelsQuery = hotelsQuery.Where(h => h.City.Contains(request.Destination) || h.Country.Contains(request.Destination));
        }
        if (request.MinPrice.HasValue)
        {
            hotelsQuery = hotelsQuery.Where(h => h.Rooms.Min(r => r.PricePerNight) >= request.MinPrice.Value);
        }
        if (request.MaxPrice.HasValue)
        {
            hotelsQuery = hotelsQuery.Where(h => h.Rooms.Min(r => r.PricePerNight) <= request.MaxPrice.Value);
        }
        if (request.CheckIn.HasValue && request.CheckOut.HasValue)
        {
            DateTime checkIn = request.CheckIn.Value;
            DateTime checkOut = request.CheckOut.Value;

            hotelsQuery = hotelsQuery.Where(h => h.Rooms.Any(r => !r.Bookings.Any(b =>
            b.Status == appDomain.BookingStatus.Confirmed || 
                     b.Status == appDomain.BookingStatus.Pending ||
                b.Status != appDomain.BookingStatus.Cancelled &&
                (checkIn < b.CheckOutDate && checkOut > b.CheckInDate)
            )));
        }
       
       
        if (request.SortBy is not null)
        {
            hotelsQuery = request.SortBy.ToLower() switch
            {
                "price low to high" => hotelsQuery.OrderBy(h => h.Rooms.Min(r => r.PricePerNight)),
                "price high to low" => hotelsQuery.OrderByDescending(h => h.Rooms.Min(r => r.PricePerNight)),
                "guest rating" => hotelsQuery.OrderByDescending(h => h.Reviews.Any() ? h.Reviews.Average(r => r.Rating) : 0),
                _ => hotelsQuery
            };
        }
        
       
        if (request.StarRating is not null && request.StarRating.Length > 0)
        {
            hotelsQuery = hotelsQuery.Where(h => h.StarRating.HasValue && request.StarRating.Contains(h.StarRating.Value));
        }

        
        List<appDomain.Hotel> filteredHotels = await _hotelservice.ExecuteHotelQueryAsync(hotelsQuery, cancellationToken);

        
        List<FilterHotelResponse> response = filteredHotels.Select(h => new FilterHotelResponse
        {
            Id = h.Id,
            Name = h.Name,
            Address = h.Address,
            City = h.City,
            Country = h.Country,
            StarRating = h.StarRating,
            MinPrice = (double)(h.Rooms.Any() ? h.Rooms.Min(r => r.PricePerNight) : 0),
            AverageRating = h.Reviews.Any() ? Math.Round(h.Reviews.Average(r => r.Rating), 1) : 0
        }).ToList();

        return response;
    }
}
