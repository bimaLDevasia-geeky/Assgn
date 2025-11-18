using System;
using System.Runtime.CompilerServices;
using Hotel.Booking.Application.Command.Login.command;
using Hotel.Booking.Application.DTOs;
using Hotel.Booking.Application.Interfaces;
using appDomain = Hotel.Booking.Domain.Entities;
using MediatR;
using Hotel.Booking.Domain.Entities;
using Hotel.Booking.Domain.Interfaces;

namespace Hotel.Booking.Application.Command.Login.handler;

public class AdminLoginCommandHandler:IRequestHandler<AdminLoginCommand, AdminResponse>
{
    private readonly ITokenService _tokenService;
    private readonly IEmployeeQueryService _employeeQueryService;

    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ICustomerQueryService _customerQueryService;
    private readonly IUnitOfWork _unitOfWork;

    public AdminLoginCommandHandler(ITokenService tokenService,IEmployeeQueryService employeeQueryService,IRefreshTokenRepository refreshTokenRepository,ICustomerQueryService customerQueryService,IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _employeeQueryService = employeeQueryService;
        _refreshTokenRepository = refreshTokenRepository;
        _customerQueryService = customerQueryService;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AdminResponse> Handle(AdminLoginCommand request, CancellationToken cancellationToken)
    {
        appDomain.Employee? employee = await _employeeQueryService.GetEmployeeByEmailAsync(request.Email, cancellationToken);
        appDomain.Customer? customer = await _customerQueryService.GetCustomerByEmailAsync(request.Email, cancellationToken);
        if (employee is not null){


        if (!BCrypt.Net.BCrypt.Verify(request.Password, employee.PasswordHash))
        {
            throw new UnauthorizedAccessException(message:"Invalid  password.");
        }
        if (employee.Role != "Admin")
        {
            throw new UnauthorizedAccessException(message:"Access denied. Admins only.");
        }

        string token = _tokenService.GenerateAccessToken(employee.Id, employee.Email, employee.Role);

        appDomain.RefreshToken refreshToken = _tokenService.GenerateRefreshToken(employeeId: employee.Id);
        await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _tokenService.SetRefreshToken(refreshToken);


        return new AdminResponse
        {
            Id = employee.Id,
            Token = token,
            FullName = employee.FullName,
            Email = employee.Email,
            Role = employee.Role
        };

    }
        else if(customer is not null)
        {
            if (!BCrypt.Net.BCrypt.Verify(request.Password, customer.PasswordHash))
            {
                throw new UnauthorizedAccessException(message:"Invalid  password.");
            }

            string token = _tokenService.GenerateAccessToken(customer.Id, customer.Email, "Customer");

            appDomain.RefreshToken refreshToken = _tokenService.GenerateRefreshToken(customerId: customer.Id);
            await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _tokenService.SetRefreshToken(refreshToken);
        
            return new AdminResponse
            {
                Id = customer.Id,
                Token = token,
                FullName = customer.FullName,
                Email = customer.Email,
                Role = "Customer"
            };
        }
        else
        {
            throw new UnauthorizedAccessException(message: "Invalid email ");
        }
}
}
