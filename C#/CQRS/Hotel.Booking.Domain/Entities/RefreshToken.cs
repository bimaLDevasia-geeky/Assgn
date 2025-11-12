using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Booking.Domain.Entities;

public class RefreshToken
{
    
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public DateTime Created { get; set; }
    public bool IsRevoked { get; set; } 
    
    
    public Guid EmployeeId { get; set; }    

    public Employee Employee { get; set; } = null!;    
}
