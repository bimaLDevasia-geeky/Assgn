# Fix query handler constructors to use HotelBookingDbContext

$entities = @("Booking", "Customer", "Employee", "Review", "Room", "RoomType")

foreach ($entity in $entities) {
    $folderPath = "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Application\Query\$entity"
    
    if (Test-Path $folderPath) {
        $files = Get-ChildItem -Path $folderPath -Filter "*QueryHandler.cs"
        
        foreach ($file in $files) {
            $content = Get-Content $file.FullName -Raw
            
            # Remove the Hotel.Booking.Domain.Interfaces using if present
            $content = $content -replace 'using Hotel\.Booking\.Domain\.Interfaces;[\r\n]+', ''
            
            # Fix constructor parameter from IBookingRepository to HotelBookingDbContext
            $content = $content -replace 'public (.+?)\(IBookingRepository bookingRepository\)', 'public $1(HotelBookingDbContext context)'
            $content = $content -replace 'public (.+?)\(ICustomerRepository customerRepository\)', 'public $1(HotelBookingDbContext context)'
            $content = $content -replace 'public (.+?)\(IEmployeeRepository employeeRepository\)', 'public $1(HotelBookingDbContext context)'
            $content = $content -replace 'public (.+?)\(IReviewRepository reviewRepository\)', 'public $1(HotelBookingDbContext context)'
            $content = $content -replace 'public (.+?)\(IRoomRepository roomRepository\)', 'public $1(HotelBookingDbContext context)'
            $content = $content -replace 'public (.+?)\(IRoomTypeRepository roomTypeRepository\)', 'public $1(HotelBookingDbContext context)'
            
            # Fix constructor body assignment
            $content = $content -replace '[\r\n\s]+IBookingRepository = bookingRepository;', "`r`n            _context = context;"
            $content = $content -replace '[\r\n\s]+_bookingRepository = bookingRepository;', "`r`n            _context = context;"
            $content = $content -replace '[\r\n\s]+_customerRepository = customerRepository;', "`r`n            _context = context;"
            $content = $content -replace '[\r\n\s]+_employeeRepository = employeeRepository;', "`r`n            _context = context;"
            $content = $content -replace '[\r\n\s]+_reviewRepository = reviewRepository;', "`r`n            _context = context;"
            $content = $content -replace '[\r\n\s]+_roomRepository = roomRepository;', "`r`n            _context = context;"
            $content = $content -replace '[\r\n\s]+_roomTypeRepository = roomTypeRepository;', "`r`n            _context = context;"
            
            Set-Content -Path $file.FullName -Value $content -NoNewline
            Write-Host "Fixed: $($file.Name)"
        }
    }
}

Write-Host "All query handler constructors fixed!"
