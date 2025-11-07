using System;
using Hotel.Booking.Domain.Interfaces;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Command.Hotel;

public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Guid>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateHotelCommandHandler(IHotelRepository repository,IUnitOfWork unitOfWork)
        {
            _hotelRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateHotelCommand request,CancellationToken ct)
        {
            appDomain.Hotel hotel = appDomain.Hotel.Create(request.Name,
                request.Address, request.City, request.PhoneNumber,
                request.Country);
            await _hotelRepository.AddAsync(hotel);
            await _unitOfWork.SaveChangesAsync(ct);
            return hotel.Id;
        }
    }