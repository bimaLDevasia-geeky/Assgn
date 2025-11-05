using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Hotel.Booking.Application.Command
{
    public class UpdateCustomerCommand : IRequest<domain.Customer>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string IdProofNumber { get; set; } = null!;
    }

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
            domain.Customer? customer = await _customerRepository.GetByIdAsync(request.Id);
            if (customer is null)
            {
                throw new KeyNotFoundException("Customer not found");
            }
            
            customer.UpdateDetails(request.FullName, request.Email, request.PhoneNumber, request.IdProofNumber);
            await _unitOfWork.SaveChangesAsync(ct);
            return customer;
        }
    }
}
