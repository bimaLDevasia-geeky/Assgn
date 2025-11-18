using System;
using Hotel.Booking.Application.DTOs;
using Hotel.Booking.Application.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Hotel.Booking.Infrastructure.Services;

public class EmailService:IEmailService
{
private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public void SendEmail(EmailDto request)
    {   
        MimeMessage email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config["EmailSettings:FromAddress"]));
        email.To.Add(MailboxAddress.Parse(request.To));
        email.Subject = request.Subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = request.Body };

        // Implementation for sending email using SMTP or any email service
        var host = _config["EmailSettings:Host"];
        var port = int.Parse(_config["EmailSettings:Port"] ?? "587");
        var userName = _config["EmailSettings:UserName"];
        var password = _config["EmailSettings:Password"];
        var fromAddress = _config["EmailSettings:FromAddress"];

        using var smtp = new SmtpClient();
        smtp.Connect(host, port, MailKit.Security.SecureSocketOptions.StartTls);
        smtp.Authenticate(userName, password);
        smtp.Send(email);
        smtp.Disconnect(true);

        
    }
}
