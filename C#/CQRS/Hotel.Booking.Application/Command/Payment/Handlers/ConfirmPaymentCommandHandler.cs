using System;
using Hotel.Booking.Application.Command.Payment.Commands;
using Hotel.Booking.Application.Interfaces;
using Hotel.Booking.Domain.Entities;
using Hotel.Booking.Domain.Interfaces;
using MediatR;
using appDomain = Hotel.Booking.Domain.Entities;

namespace Hotel.Booking.Application.Command.Payment.Handlers;

public class ConfirmPaymentCommandHandler:IRequestHandler<ConfirmPaymentCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPaymentService _paymentService;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IBookingQueryService _bookingQueryService;

    public ConfirmPaymentCommandHandler(IUnitOfWork unitOfWork, IBookingQueryService bookingQueryService, IPaymentService paymentService, IPaymentRepository paymentRepository)
    {
        _unitOfWork = unitOfWork;
        _bookingQueryService = bookingQueryService;
        _paymentService = paymentService;
        _paymentRepository = paymentRepository;
    }

    public async Task<bool> Handle(ConfirmPaymentCommand request, CancellationToken ct)
    {
        Console.WriteLine("=== ConfirmPaymentCommandHandler START ===");
        Console.WriteLine($"BookingId: {request.BookingId}");
        Console.WriteLine($"PaymentId: '{request.PaymentId}'");
        Console.WriteLine($"OrderId: '{request.OrderId}'");
        Console.WriteLine($"Signature: '{request.Signature}'");
        
        try
        {
            // Step 1: Verify signature
      
            bool isValid = _paymentService.VerifySignature(
                request.PaymentId,
                request.OrderId,
                request.Signature
            );

            

            if (!isValid)
            {
              
                throw new Exception("Payment Verification Failed. Signature mismatch.");
            }
            

            Console.WriteLine("Step 2: Getting booking...");
            appDomain.Booking? booking = await _bookingQueryService.GetBookingByIdAsync(request.BookingId, ct);
            if (booking is null)
            {
              
                throw new Exception("Booking not found");
            }
           
            
            
            
            appDomain.Payment? payment = appDomain.Payment.Create(
                booking.Id,
                booking.TotalAmount,
                request.PaymentId
            );
            await _paymentRepository.AddAsync(payment);
          
            PaymentStatus status = PaymentStatus.Paid;
            payment.UpdateDetails(status, null, null);
            
            // Step 5: Update booking status
          
            BookingStatus bookingStatus = BookingStatus.Confirmed;
            booking.UpdateBookingDetails(bookingStatus, null, null, null, null);
            
            // Step 6: Save changes
         
            await _unitOfWork.SaveChangesAsync(ct);
            
            Console.WriteLine("SUCCESS: Payment confirmed successfully");
            Console.WriteLine("=== ConfirmPaymentCommandHandler END ===");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR in ConfirmPaymentCommandHandler: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            Console.WriteLine("=== ConfirmPaymentCommandHandler END (ERROR) ===");
            throw;
        }
    }
}