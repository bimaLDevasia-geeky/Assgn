using System;
using Hotel.Booking.Application.Command.Hotel.Commands;
using Hotel.Booking.Domain.Interfaces;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Command.Hotel.Handlers;

public class DeleteHotelCommandHandler:IRequestHandler< DeleteHotelCommand,bool>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteHotelCommandHandler(IHotelRepository hotelRepository, IUnitOfWork unitOfWork)
    {
        _hotelRepository = hotelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        appDomain.Hotel? hotel = await _hotelRepository.GetByIdAsync(request.Id, cancellationToken);
        if (hotel is null)
        {   
            throw new KeyNotFoundException("Hotel not found");
        }

        _hotelRepository.Delete(hotel);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
