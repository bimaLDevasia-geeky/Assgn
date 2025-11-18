using System;
using Hotel.Booking.Application.Interfaces;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
namespace Hotel.Booking.Application.Query.Employee;

public class GetEmployeeByHotelIdQueryHandler:IRequestHandler<GetEmployeeByHotelIdQuery, List<appDomain.Employee>>
{
    private readonly IEmployeeQueryService _employeeQueryService;
    public GetEmployeeByHotelIdQueryHandler(IEmployeeQueryService employeeQueryService)
    {
        _employeeQueryService = employeeQueryService;
    }

    public async Task<List<appDomain.Employee>> Handle(GetEmployeeByHotelIdQuery request, CancellationToken cancellationToken)
    {
        return await _employeeQueryService.GetEmployeeByHotelIdAsync(request.HotelId);
    }
}
    