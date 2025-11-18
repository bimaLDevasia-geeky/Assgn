using System;
using Hotel.Booking.Application.Interfaces;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;
namespace Hotel.Booking.Application.Query.Customer;

public class GetCustomerByEmailHandler:IRequestHandler<GetCustomerByEmail, appDomain.Customer?>
{
    private readonly ICustomerQueryService _customerQueryService;
    public GetCustomerByEmailHandler(ICustomerQueryService customerQueryService)
    {
        _customerQueryService = customerQueryService;
    }

    public async Task<appDomain.Customer?> Handle(GetCustomerByEmail request, CancellationToken cancellationToken)
    {
        return await _customerQueryService.GetCustomerByEmailAsync(request.Email, cancellationToken);
    }
}