using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Customer.Commands;

namespace Hotel.Booking.Application.Command.Customer.Handlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _customerRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken ct)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id, ct);
            if (customer is null)
            {
                throw new KeyNotFoundException("Customer not found");
            }

            _customerRepository.Delete(customer);
            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }
    }
}

