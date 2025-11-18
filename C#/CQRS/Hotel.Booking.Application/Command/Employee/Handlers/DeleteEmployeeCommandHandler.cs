using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Employee.Commands;

namespace Hotel.Booking.Application.Command.Employee.Handlers
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandler(IEmployeeRepository repository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken ct)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id, ct);
            if (employee is null)
            {
                throw new KeyNotFoundException("Employee not found");
            }

            _employeeRepository.Delete(employee);
            await _unitOfWork.SaveChangesAsync(ct);
            return true;
        }
    }
}

