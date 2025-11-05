using System;
using Hotel.Booking.Domain.Interfaces;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
namespace Hotel.Booking.Application.Command;

public class UpdateHotelCommandHandler(IHotelRepository _hotelrepository,IUnitOfWork _unitofwork) : IRequestHandler<UpdateHotelCommand, appDomain.Hotel> 
    {
        public async Task<appDomain.Hotel> Handle(UpdateHotelCommand request,CancellationToken ct)
        {
            try
            {
                appDomain.Hotel? hotel = await _hotelrepository.GetByIdAsync(request.Id);
                if (hotel is null)
                {
                    throw new KeyNotFoundException("Hotel Not found");
                }
                hotel.UpdateDetails(request.Name, request.Address, request.City, request.Country, request.PhoneNumber);
                await _unitofwork.SaveChangesAsync();
                return hotel;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new ApplicationException("An error occurred while updating the hotel.", ex);
            }
            

        }
    }
