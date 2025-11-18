namespace Hotel.Booking.Application.DTOs;

public class  AdminResponse
{
    public Guid Id { get; set; }

    public string Token { get; set; } = null!;
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
