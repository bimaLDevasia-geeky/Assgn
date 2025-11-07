using Hotel.Booking.Domain.Interfaces;
using domain = Hotel.Booking.Domain.Entities;
using MediatR;
using System;
using System.Text.Json.Serialization;

namespace Hotel.Booking.Application.Command.Employee
{
    public class UpdateEmployeeCommand : IRequest<domain.Employee>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }

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
