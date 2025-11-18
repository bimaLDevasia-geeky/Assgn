using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Employee.Commands;

namespace Hotel.Booking.Application.Command.Employee.Handlers
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, domain.Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeCommandHandler(IEmployeeRepository repository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<domain.Employee> Handle(UpdateEmployeeCommand request, CancellationToken ct)
        {
            domain.Employee? employee = await _employeeRepository.GetByIdAsync(request.Id, ct);
            if (employee is null)
            {
                throw new KeyNotFoundException("Employee not found");
            }
            
            employee.UpdateDetails(request.FullName, request.Email, request.Role);
            await _unitOfWork.SaveChangesAsync(ct);
            return employee;
        }
    }
}

