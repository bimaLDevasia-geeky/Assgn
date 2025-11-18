using System;
using Hotel.Booking.Application.Command.Email.Command;
using Hotel.Booking.Application.DTOs;
using Hotel.Booking.Application.Interfaces;
using MediatR;

namespace Hotel.Booking.Application.Command.Email.Handler;

public class EmailCommandHandler:IRequestHandler<EmailCommand,bool>
{
    private readonly IEmailService _emailService;
    public EmailCommandHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }
    public Task<bool> Handle(EmailCommand request, CancellationToken cancellationToken)
    {
        
        EmailDto dto = new EmailDto 
        { 
            To = request.To, 
            Subject = request.Subject, 
            Body = request.Body 
        };

        
        _emailService.SendEmail(dto);

        return Task.FromResult(true);
    }

}
