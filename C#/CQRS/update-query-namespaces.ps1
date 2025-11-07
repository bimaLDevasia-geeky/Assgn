# Script to update namespaces in Query files

$basePath = "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Application\Query"

# Function to update namespace in a file
function Update-Namespace {
    param(
        [string]$filePath,
        [string]$newNamespace
    )
    
    $content = Get-Content $filePath -Raw
    $content = $content -replace 'namespace Hotel\.Booking\.Application\.Query\s*{', "namespace $newNamespace`n{"
    $content = $content -replace 'namespace Hotel\.Booking\.Application\.Query;', "namespace $newNamespace;"
    
    Set-Content $filePath -Value $content -NoNewline
    Write-Host "Updated namespace in: $filePath"
}

# Update all Query files in subfolders
$folders = @("Payment", "Review", "Room", "RoomType", "Employee", "Customer", "Booking")

foreach ($folder in $folders) {
    $files = Get-ChildItem "$basePath\$folder\*.cs"
    foreach ($file in $files) {
        Update-Namespace $file.FullName "Hotel.Booking.Application.Query.$folder"
    }
}

Write-Host "`nAll query file namespaces updated!"
