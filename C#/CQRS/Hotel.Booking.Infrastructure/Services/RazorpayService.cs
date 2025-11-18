using System;
using System.Security.Cryptography;
using System.Text;
using Hotel.Booking.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;

namespace Hotel.Booking.Infrastructure.Services;

public class RazorpayService:IPaymentService
{
    private readonly string? _key;
    private readonly string? _secret;

    public RazorpayService(IConfiguration config)
    {
        _key = config["Razorpay:Key"];
        _secret = config["Razorpay:Secret"];
    }
    public Task<string> CreateOrderAsync(decimal amount)
    {
        
        RazorpayClient client = new RazorpayClient(_key, _secret);
        Dictionary<string, object> options = new Dictionary<string, object>
        {
            { "amount", amount * 100 }, // Convert Rupee to Paise
            { "currency", "INR" },
            { "payment_capture", 1 } // Auto-capture
        };
        Order order = client.Order.Create(options);
        return Task.FromResult(order["id"].ToString());
    }
    public bool VerifySignature(string paymentId, string orderId, string signature)
    {
        Console.WriteLine("=== VerifySignature START ===");
        Console.WriteLine($"PaymentId: '{paymentId}'");
        Console.WriteLine($"OrderId: '{orderId}'");
        Console.WriteLine($"Signature: '{signature}'");
        Console.WriteLine($"Secret configured: {!string.IsNullOrWhiteSpace(_secret)}");
        
        try
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(paymentId))
            {
                Console.WriteLine("ERROR: PaymentId is null or empty");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(orderId))
            {
                Console.WriteLine("ERROR: OrderId is null or empty");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(signature))
            {
                Console.WriteLine("ERROR: Signature is null or empty");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_secret))
            {
                Console.WriteLine("ERROR: Razorpay secret is not configured");
                return false;
            }

            // Manual HMAC-SHA256 verification (most reliable method)
            string payload = $"{orderId}|{paymentId}";
            Console.WriteLine($"Payload for verification: '{payload}'");
            
            string generatedSignature = GenerateSignature(payload, _secret);
            Console.WriteLine($"Generated signature: '{generatedSignature}'");
            Console.WriteLine($"Received signature:  '{signature}'");

            bool isValid = generatedSignature.Equals(signature, StringComparison.OrdinalIgnoreCase);
            
            Console.WriteLine($"Signature match: {isValid}");
            Console.WriteLine("=== VerifySignature END ===");
            
            return isValid;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR in VerifySignature: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            Console.WriteLine("=== VerifySignature END (ERROR) ===");
            return false;
        }
    }

    private string GenerateSignature(string payload, string secret)
    {
        var encoding = new UTF8Encoding();
        byte[] keyBytes = encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(payload);

        using (var hmac = new HMACSHA256(keyBytes))
        {
            byte[] hashBytes = hmac.ComputeHash(messageBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
