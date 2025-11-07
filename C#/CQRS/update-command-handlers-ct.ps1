# Script to ensure all command handlers pass CancellationToken to repository calls

$basePath = "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Application\Command"

function Update-CommandHandler {
    param([string]$filePath)
    
    $content = Get-Content $filePath -Raw
    
    # Update GetByIdAsync calls to include cancellationToken/ct
    $content = $content -replace '\.GetByIdAsync\(([^)]+)\)(?!\s*,\s*(cancellationToken|ct))', '.GetByIdAsync($1, cancellationToken)'
    $content = $content -replace '\.GetByIdAsync\(([^)]+),\s*ct\)', '.GetByIdAsync($1, cancellationToken)'
    
    # Update GetAllAsync calls to include cancellationToken
    $content = $content -replace '\.GetAllAsync\(\)(?!\s*,\s*cancellationToken)', '.GetAllAsync(cancellationToken)'
    
    Set-Content $filePath -Value $content -NoNewline
    Write-Host "Updated: $filePath"
}

# Process all command handler files
$folders = @("Hotel", "Booking", "Customer", "Employee", "Payment", "Review", "Room", "RoomType")

foreach ($folder in $folders) {
    $files = Get-ChildItem "$basePath\$folder\*CommandHandler.cs" -ErrorAction SilentlyContinue
    if ($files) {
        foreach ($file in $files) {
            Update-CommandHandler $file.FullName
        }
    }
    
    # Also update command files that have handlers embedded
    $commandFiles = Get-ChildItem "$basePath\$folder\*Command.cs" -ErrorAction SilentlyContinue
    if ($commandFiles) {
        foreach ($file in $commandFiles) {
            $content = Get-Content $file.FullName -Raw
            if ($content -match 'IRequestHandler') {
                Update-CommandHandler $file.FullName
            }
        }
    }
}

Write-Host "`nAll command handlers updated with CancellationToken!"
