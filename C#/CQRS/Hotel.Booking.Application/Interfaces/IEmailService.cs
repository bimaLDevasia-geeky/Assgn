using System;
using Hotel.Booking.Application.DTOs;

namespace Hotel.Booking.Application.Interfaces;

public interface IEmailService
{
    void SendEmail(EmailDto request);
}
