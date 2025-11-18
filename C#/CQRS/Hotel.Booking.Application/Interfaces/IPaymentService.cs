using System;

namespace Hotel.Booking.Application.Interfaces;

public interface IPaymentService
{
    Task<string> CreateOrderAsync(decimal amount);
    bool VerifySignature(string paymentId, string orderId, string signature);
}
