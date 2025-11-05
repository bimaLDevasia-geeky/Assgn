using System;

namespace Hotel.Booking.Domain.Entities;

public enum PaymentStatus{
    Pending,
    Paid,
    Failed
}

public enum PaymentMethod{
    Cash,
    Card,
    UPI,
    Online
}

public class Payment
{
    public Guid Id { get; private set; }
    public Guid BookingId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime PaymentDate { get; private set; }
    public PaymentMethod PaymentMethod { get; private set; }
    public PaymentStatus Status { get; private set; }

    public Booking Booking { get; private set; } = null!;

    public static Payment Create(Guid bookingId, decimal amount, PaymentMethod paymentMethod)
    {
        return new Payment
        {
            BookingId = bookingId,
            Amount = amount,
            PaymentDate = DateTime.UtcNow,
            PaymentMethod = paymentMethod,
            Status = PaymentStatus.Pending
        };
    }

    public void UpdateDetails(PaymentStatus? status, decimal? amount, PaymentMethod? paymentMethod)
    {
        if (status.HasValue)
        {
            Status = status.Value;
        }
        if (amount.HasValue && amount > 0)
        {
            Amount = amount.Value;
        }
        if (paymentMethod.HasValue)
        {
            PaymentMethod = paymentMethod.Value;
        }
    }   
}
