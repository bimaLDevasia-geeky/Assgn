using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Customer.Commands;

namespace Hotel.Booking.Application.Command.Customer.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _customerRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken ct)
        {   
            List<domain.Customer> existingCustomer = await _customerRepository.GetAllAsync();
            if (existingCustomer.Any(c => c.Email == request.Email))
            {
                throw new Exception("Customer with the same email already exists");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.PasswordHash);
            domain.Customer customer = domain.Customer.Create(
                request.FullName,
                request.Email,
                request.PhoneNumber,
                request.IdProofNumber,
                passwordHash
            );
            
            await _customerRepository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync(ct);
            return customer.Id;
        }
    }
}

