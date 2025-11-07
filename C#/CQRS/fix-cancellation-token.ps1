# Script to fix cancellationToken parameter name mismatches

$files = @(
    "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Application\Command\Customer\UpdateCustomerCommand.cs",
    "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Application\Command\Employee\UpdateEmployeeCommand.cs",
    "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Application\Command\Payment\DeletePaymentCommand.cs",
    "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Application\Command\Payment\UpdatePaymentCommand.cs",
    "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Application\Command\RoomType\UpdateRoomTypeCommand.cs"
)

foreach ($file in $files) {
    $content = Get-Content $file -Raw
    
    # Replace cancellationToken with ct in GetByIdAsync calls (but only in the argument, not the parameter declaration)
    $content = $content -replace '\.GetByIdAsync\(request\.Id, cancellationToken\)', '.GetByIdAsync(request.Id, ct)'
    
    Set-Content $file -Value $content -NoNewline
    Write-Host "Fixed: $file"
}

Write-Host "`nAll cancellationToken references fixed!"
