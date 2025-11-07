# Script to fix ALL cancellationToken parameter name mismatches in Command folder

$basePath = "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Application\Command"

# Get all .cs files in Command folder and subfolders
$files = Get-ChildItem -Path $basePath -Filter "*.cs" -Recurse

foreach ($file in $files) {
    $content = Get-Content $file.FullName -Raw
    $originalContent = $content
    
    # Replace cancellationToken with ct in GetByIdAsync calls (but only in the argument, not the parameter declaration)
    $content = $content -replace '\.GetByIdAsync\(request\.Id, cancellationToken\)', '.GetByIdAsync(request.Id, ct)'
    
    # Also fix if there are any GetAllAsync calls with cancellationToken
    $content = $content -replace '\.GetAllAsync\(cancellationToken\)', '.GetAllAsync(ct)'
    
    if ($content -ne $originalContent) {
        Set-Content $file.FullName -Value $content -NoNewline
        Write-Host "Fixed: $($file.FullName)"
    }
}

Write-Host "`nAll cancellationToken references in Command folder fixed!"
