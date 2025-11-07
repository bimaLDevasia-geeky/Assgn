using Hotel.Booking.Domain.Interfaces;
using MediatR;
using System;

namespace Hotel.Booking.Application.Command.Employee
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

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
