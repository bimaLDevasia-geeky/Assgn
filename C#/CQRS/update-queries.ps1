# Script to update all Query handlers to use DbContext instead of repositories

$basePath = "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Application\Query"

# Function to update a query handler file
function Update-QueryHandler {
    param(
        [string]$filePath,
        [string]$entity,
        [string]$repositoryInterface,
        [string]$repositoryField,
        [string]$dbSetName
    )
    
    $content = Get-Content $filePath -Raw
    
    # Update using statements
    $content = $content -replace 'using Hotel\.Booking\.Domain\.Interfaces;', 'using Hotel.Booking.Infrastructure.Persistance;'
    $content = $content -replace 'using appDomain = Hotel\.Booking\.Domain\.Entities;', @"
using Microsoft.EntityFrameworkCore;
using Hotel.Booking.Infrastructure.Persistance;
using appDomain = Hotel.Booking.Domain.Entities;
"@
    
    # Update field and constructor
    $content = $content -replace "private readonly $repositoryInterface $repositoryField;", "private readonly HotelBookingDbContext _context;"
    $content = $content -replace "public (\w+)\($repositoryInterface $repositoryField\)", 'public $1(HotelBookingDbContext context)'
    $content = $content -replace "$repositoryField = $repositoryField;", "_context = context;"
    
    # Update GetByIdAsync calls
    $content = $content -replace "await $repositoryField\.GetByIdAsync\(request\.Id\)", "await _context.$dbSetName.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)"
    
    # Update GetAllAsync calls
    $content = $content -replace "await $repositoryField\.GetAllAsync\(\)", "await _context.$dbSetName.ToListAsync(cancellationToken)"
    
    Set-Content $filePath -Value $content -NoNewline
    Write-Host "Updated: $filePath"
}

# Update Review queries
Update-QueryHandler "$basePath\Review\GetReviewByIdQueryHandler.cs" "Review" "IReviewRepository" "_reviewRepository" "Reviews"
Update-QueryHandler "$basePath\Review\GetAllReviewsQueryHandler.cs" "Review" "IReviewRepository" "_reviewRepository" "Reviews"

# Update Room queries
Update-QueryHandler "$basePath\Room\GetRoomByIdQueryHandler.cs" "Room" "IRoomRepository" "_roomRepository" "Rooms"
Update-QueryHandler "$basePath\Room\GetAllRoomsQueryHandler.cs" "Room" "IRoomRepository" "_roomRepository" "Rooms"

# Update RoomType queries
Update-QueryHandler "$basePath\RoomType\GetRoomTypeByIdQueryHandler.cs" "RoomType" "IRoomTypeRepository" "_roomTypeRepository" "RoomTypes"
Update-QueryHandler "$basePath\RoomType\GetAllRoomTypesQueryHandler.cs" "RoomType" "IRoomTypeRepository" "_roomTypeRepository" "RoomTypes"

# Update Employee queries (already in folder)
Update-QueryHandler "$basePath\Employee\GetEmployeeByIdQueryHandler.cs" "Employee" "IEmployeeRepository" "_employeeRepository" "Employees"
Update-QueryHandler "$basePath\Employee\GetAllEmployeesQueryHandler.cs" "Employee" "IEmployeeRepository" "_employeeRepository" "Employees"

# Update Customer queries (already in folder)
Update-QueryHandler "$basePath\Customer\GetCustomerByIdQueryHandler.cs" "Customer" "ICustomerRepository" "_customerRepository" "Customers"
Update-QueryHandler "$basePath\Customer\GetAllCustomersQueryHandler.cs" "Customer" "ICustomerRepository" "_customerRepository" "Customers"

# Update Booking queries (already in folder)
Update-QueryHandler "$basePath\Booking\GetBookingByIdQueryHandler.cs" "Booking" "IBookingRepository" "_bookingRepository" "Bookings"
Update-QueryHandler "$basePath\Booking\GetAllBookingsQueryHandler.cs" "Booking" "IBookingRepository" "_bookingRepository" "Bookings"

Write-Host "`nAll query handlers updated!"
