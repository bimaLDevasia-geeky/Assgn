using System;

namespace Hotel.Booking.Domain.Entities;

public class RoomType
{
    public Guid Id { get; private set; }
    public string TypeName { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int Capacity { get; private set; }

    public ICollection<Room> Rooms { get; private set; } = new List<Room>();

    public static RoomType Create(string typeName, string description, int capacity)
    {
        return new RoomType
        {
            TypeName = typeName,
            Description = description,
            Capacity = capacity
        };
    }

    public void UpdateDetails(string? typeName, string? description, int? capacity)
    {
        if (!string.IsNullOrWhiteSpace(typeName))
        {
            TypeName = typeName;
        }
        if (!string.IsNullOrWhiteSpace(description))
        {
            Description = description;
        }
        if (capacity.HasValue && capacity > 0)
        {
            Capacity = capacity.Value;
        }
    }
}
