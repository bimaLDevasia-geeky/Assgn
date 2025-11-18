using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Customer.Commands;

namespace Hotel.Booking.Application.Command.Customer.Handlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, domain.Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _customerRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<domain.Customer> Handle(UpdateCustomerCommand request, CancellationToken ct)
        {
            domain.Customer? customer = await _customerRepository.GetByIdAsync(request.Id, ct);
            if (customer is null)
            {
                throw new KeyNotFoundException("Customer not found");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash);
            customer.UpdateDetails(request.FullName, request.Email, request.PhoneNumber, request.IdProofNumber, passwordHash);
            await _unitOfWork.SaveChangesAsync(ct);
            return customer;
        }
    }
}

