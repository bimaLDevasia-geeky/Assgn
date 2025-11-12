using Hotel.Booking.Domain.Interfaces;
using MediatR;
using domain = Hotel.Booking.Domain.Entities;
using System;

using Hotel.Booking.Application.Command.Employee.Commands;

namespace Hotel.Booking.Application.Command.Employee.Handlers
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeCommandHandler(IEmployeeRepository repository, IUnitOfWork unitOfWork)
        {
            _employeeRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken ct)
        {
            domain.Employee employee = domain.Employee.Create(
                request.FullName,
                request.Email,
                request.Role,
                request.HotelId
            );
            
            await _employeeRepository.AddAsync(employee);
            await _unitOfWork.SaveChangesAsync(ct);
            return employee.Id;
        }
    }
}

