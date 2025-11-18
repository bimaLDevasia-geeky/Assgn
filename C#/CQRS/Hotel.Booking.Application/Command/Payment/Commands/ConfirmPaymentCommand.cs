using System;
using MediatR;

namespace Hotel.Booking.Application.Command.Payment.Commands;

public class ConfirmPaymentCommand:IRequest<bool>
{
    public Guid BookingId { get; set; }
    public string PaymentId { get; set; } = null!;
    public string OrderId { get; set; } = null!;
    public string Signature { get; set; } = null!;
}
