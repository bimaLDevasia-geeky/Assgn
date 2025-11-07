# Script to clean up duplicate usings and remove leftover repository references in query handlers

$basePath = "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Application\Query"

function Clean-QueryHandler {
    param([string]$filePath)
    
    $content = Get-Content $filePath -Raw
    
    # Remove Hotel.Booking.Domain.Interfaces using
    $content = $content -replace 'using Hotel\.Booking\.Domain\.Interfaces;\s*[\r\n]+', ''
    
    # Remove duplicate using statements
    $lines = $content -split "`r`n"
    $uniqueLines = @()
    $seenUsings = @{}
    
    foreach ($line in $lines) {
        if ($line -match 'using (.+);') {
            $usingStatement = $matches[1]
            if (-not $seenUsings.ContainsKey($usingStatement)) {
                $uniqueLines += $line
                $seenUsings[$usingStatement] = $true
            }
        } else {
            $uniqueLines += $line
        }
    }
    
    $content = $uniqueLines -join "`r`n"
    
    Set-Content $filePath -Value $content -NoNewline
    Write-Host "Cleaned: $filePath"
}

# Process all query handler files
$folders = @("Payment", "Review", "Room", "RoomType", "Employee", "Customer", "Booking")

foreach ($folder in $folders) {
    $files = Get-ChildItem "$basePath\$folder\*QueryHandler.cs" -ErrorAction SilentlyContinue
    if ($files) {
        foreach ($file in $files) {
            Clean-QueryHandler $file.FullName
        }
    }
}

Write-Host "`nAll query handlers cleaned!"
