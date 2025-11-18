# PowerShell script to update query handlers to use services instead of DbContext

$basePath = "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Application\Query"

# Define mappings for each entity
$entityMappings = @{
    "Hotel" = @{
        "Service" = "IHotelQueryService"
        "GetById" = "GetHotelByIdAsync"
        "GetAll" = "GetAllHotelsAsync"
    }
    "Booking" = @{
        "Service" = "IBookingQueryService"
        "GetById" = "GetBookingByIdAsync"
        "GetAll" = "GetAllBookingsAsync"
    }
    "Customer" = @{
        "Service" = "ICustomerQueryService"
        "GetById" = "GetCustomerByIdAsync"
        "GetAll" = "GetAllCustomersAsync"
    }
    "Employee" = @{
        "Service" = "IEmployeeQueryService"
        "GetById" = "GetEmployeeByIdAsync"
        "GetAll" = "GetAllEmployeesAsync"
    }
    "Payment" = @{
        "Service" = "IPaymentQueryService"
        "GetById" = "GetPaymentByIdAsync"
        "GetAll" = "GetAllPaymentsAsync"
    }
    "Review" = @{
        "Service" = "IReviewQueryService"
        "GetById" = "GetReviewByIdAsync"
        "GetAll" = "GetAllReviewsAsync"
        "GetAllOfHotel" = "GetAllReviewsOfHotelAsync"
    }
    "Room" = @{
        "Service" = "IRoomQueryService"
        "GetById" = "GetRoomByIdAsync"
        "GetAll" = "GetAllRoomsAsync"
        "GetByHotelId" = "GetRoomsByHotelIdAsync"
    }
    "RoomType" = @{
        "Service" = "IRoomTypeQueryService"
        "GetById" = "GetRoomTypeByIdAsync"
        "GetAll" = "GetAllRoomTypesAsync"
    }
}

foreach ($entity in $entityMappings.Keys) {
    $entityPath = Join-Path $basePath $entity
    if (Test-Path $entityPath) {
        $files = Get-ChildItem -Path $entityPath -Filter "*QueryHandler.cs"
        
        foreach ($file in $files) {
            Write-Host "Processing: $($file.Name)" -ForegroundColor Cyan
            $content = Get-Content $file.FullName -Raw
            
            # Remove HotelBookingDbContext import and add Application.Interfaces
            $content = $content -replace 'using Hotel\.Booking\.Infrastructure\.Persistance;', 'using Hotel.Booking.Application.Interfaces;'
            
            # Also ensure Microsoft.EntityFrameworkCore is removed if it's the only EF using statement
            if ($content -notmatch '\.Include\(') {
                $content = $content -replace 'using Microsoft\.EntityFrameworkCore;\r?\n', ''
            }
            
            # Replace constructor injection - handle both primary and regular constructors
            $serviceName = $entityMappings[$entity]["Service"]
            
            # Pattern 1: Primary constructor like: public class Handler(HotelBookingDbContext context)
            $content = $content -replace "public class (\w+)\(HotelBookingDbContext context\)", "public class `$1($serviceName service)"
            
            # Pattern 2: Regular constructor with field
            $content = $content -replace "private readonly HotelBookingDbContext _context;", "private readonly $serviceName _service;"
            $content = $content -replace "public (\w+)\(HotelBookingDbContext context\)", "public `$1($serviceName service)"
            $content = $content -replace "\s+_context = context;", "`n        _service = service;"
            
            # Replace DbContext calls with service calls based on the handler type
            if ($file.Name -match "GetAllHotel") {
                $methodName = $entityMappings[$entity]["GetAll"]
                $content = $content -replace "await context\.Hotels\.ToListAsync\(cancellationToken\)", "await service.$methodName(cancellationToken)"
                $content = $content -replace "await _context\.Hotels\.ToListAsync\(cancellationToken\)", "await _service.$methodName(cancellationToken)"
            }
            elseif ($file.Name -match "GetHotelById") {
                $methodName = $entityMappings[$entity]["GetById"]
                $content = $content -replace "await context\.Hotels\.FirstOrDefaultAsync\(h => h\.Id == request\.Id, ct\)", "await service.$methodName(request.Id, ct)"
                $content = $content -replace "await _context\.Hotels\.FirstOrDefaultAsync\(x => x\.Id == request\.Id, cancellationToken\)", "await _service.$methodName(request.Id, cancellationToken)"
            }
            elseif ($file.Name -match "GetAllBooking") {
                $methodName = $entityMappings[$entity]["GetAll"]
                $content = $content -replace "await _context\.Bookings\.ToListAsync\(cancellationToken\)", "await _service.$methodName(cancellationToken)"
            }
            elseif ($file.Name -match "GetBookingById") {
                $methodName = $entityMappings[$entity]["GetById"]
                $content = $content -replace "await _context\.Bookings\.FirstOrDefaultAsync\(b => b\.Id == request\.Id, cancellationToken\)", "await _service.$methodName(request.Id, cancellationToken)"
            }
            elseif ($file.Name -match "GetAllCustomer") {
                $methodName = $entityMappings[$entity]["GetAll"]
                $content = $content -replace "await _context\.Customers\.ToListAsync\(cancellationToken\)", "await _service.$methodName(cancellationToken)"
            }
            elseif ($file.Name -match "GetCustomerById") {
                $methodName = $entityMappings[$entity]["GetById"]
                $content = $content -replace "await _context\.Customers\.FirstOrDefaultAsync\(x => x\.Id == request\.Id, cancellationToken\)", "await _service.$methodName(request.Id, cancellationToken)"
            }
            elseif ($file.Name -match "GetAllEmployee") {
                $methodName = $entityMappings[$entity]["GetAll"]
                $content = $content -replace "await _context\.Employees\.ToListAsync\(cancellationToken\)", "await _service.$methodName(cancellationToken)"
            }
            elseif ($file.Name -match "GetEmployeeById") {
                $methodName = $entityMappings[$entity]["GetById"]
                $content = $content -replace "await _context\.Employees\.FirstOrDefaultAsync\(e => e\.Id == request\.Id, cancellationToken\)", "await _service.$methodName(request.Id, cancellationToken)"
            }
            elseif ($file.Name -match "GetAllPayment") {
                $methodName = $entityMappings[$entity]["GetAll"]
                $content = $content -replace "await _context\.Payments\.ToListAsync\(cancellationToken\)", "await _service.$methodName(cancellationToken)"
            }
            elseif ($file.Name -match "GetPaymentById") {
                $methodName = $entityMappings[$entity]["GetById"]
                $content = $content -replace "await _context\.Payments\.FirstOrDefaultAsync\(p => p\.Id == request\.Id, cancellationToken\)", "await _service.$methodName(request.Id, cancellationToken)"
            }
            elseif ($file.Name -match "GetAllReviewOfHotel") {
                $methodName = $entityMappings[$entity]["GetAllOfHotel"]
                $content = $content -replace "await _context\.Reviews\s+\.Where\(r => r\.HotelId == request\.HotelId\)\.ToListAsync\(cancellationToken\)", "await _service.$methodName(request.HotelId, cancellationToken)"
            }
            elseif ($file.Name -match "GetAllReview") {
                $methodName = $entityMappings[$entity]["GetAll"]
                $content = $content -replace "await _context\.Reviews\.ToListAsync\(cancellationToken\)", "await _service.$methodName(cancellationToken)"
            }
            elseif ($file.Name -match "GetReviewById") {
                $methodName = $entityMappings[$entity]["GetById"]
                $content = $content -replace "await _context\.Reviews\.FirstOrDefaultAsync\(r => r\.Id == request\.Id, cancellationToken\)", "await _service.$methodName(request.Id, cancellationToken)"
            }
            elseif ($file.Name -match "GetRoomsByHostelId" -or $file.Name -match "GetRoomsByHotelId") {
                $methodName = $entityMappings[$entity]["GetByHotelId"]
                $content = $content -replace "return await _context\.Rooms\s+\.Where\(r => r\.HotelId == request\.HotelId\)\.Include\(r=>r\.RoomType\)\.ToListAsync\(cancellationToken\);", "return await _service.$methodName(request.HotelId, cancellationToken);"
            }
            elseif ($file.Name -match "GetAllRoom") {
                $methodName = $entityMappings[$entity]["GetAll"]
                $content = $content -replace "await _context\.Rooms\.ToListAsync\(cancellationToken\)", "await _service.$methodName(cancellationToken)"
            }
            elseif ($file.Name -match "GetRoomById") {
                $methodName = $entityMappings[$entity]["GetById"]
                $content = $content -replace "await _context\.Rooms\.FirstOrDefaultAsync\(r => r\.Id == request\.Id, cancellationToken\)", "await _service.$methodName(request.Id, cancellationToken)"
            }
            elseif ($file.Name -match "GetAllRoomType") {
                $methodName = $entityMappings[$entity]["GetAll"]
                $content = $content -replace "await _context\.RoomTypes\.ToListAsync\(cancellationToken\)", "await _service.$methodName(cancellationToken)"
            }
            elseif ($file.Name -match "GetRoomTypeById") {
                $methodName = $entityMappings[$entity]["GetById"]
                $content = $content -replace "await _context\.RoomTypes\.FirstOrDefaultAsync\(rt => rt\.Id == request\.Id, cancellationToken\)", "await _service.$methodName(request.Id, cancellationToken)"
            }
            
            Set-Content -Path $file.FullName -Value $content -NoNewline
            Write-Host "  Updated: $($file.Name)" -ForegroundColor Green
        }
    }
}

Write-Host "`nAll query handlers updated successfully!" -ForegroundColor Green
