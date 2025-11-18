using System;
using System.Runtime.CompilerServices;
using Hotel.Booking.Application.Interfaces;
using Hotel.Booking.Domain.Interfaces;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Command.Register.Handler;

public class AdminRegisterCommandHandler : IRequestHandler<AdminRegisterCommand, appDomain.Employee>
{
    private readonly ITokenService _tokenService;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdminRegisterCommandHandler(ITokenService tokenService, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<appDomain.Employee> Handle(AdminRegisterCommand request, CancellationToken cancellationToken)
    {
        List<appDomain.Employee> employees = await _employeeRepository.GetAllAsync(cancellationToken);

        if (employees.Any(e => e.Email == request.Email))
        {
            throw new Exception("Employee with the same email already exists");
        }
        // Admin doesn't need to be associated with a specific hotel during registration
        Guid? hotelId = null;

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        appDomain.Employee admin = appDomain.Employee.Create(
            request.FullName,
            request.Email,
            request.Role,
            hotelId,
            passwordHash
        );

         await _employeeRepository.AddAsync(admin);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return admin;

    }

}
