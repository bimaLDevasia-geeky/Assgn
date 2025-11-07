# Script to update namespaces in Command files

$basePath = "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Application\Command"

# Function to update namespace in a file
function Update-Namespace {
    param(
        [string]$filePath,
        [string]$newNamespace
    )
    
    $content = Get-Content $filePath -Raw
    $content = $content -replace 'namespace Hotel\.Booking\.Application\.Command\s*{', "namespace $newNamespace`n{"
    $content = $content -replace 'namespace Hotel\.Booking\.Application\.Command;', "namespace $newNamespace;"
    
    Set-Content $filePath -Value $content -NoNewline
    Write-Host "Updated namespace in: $filePath"
}

# Update all Command files in subfolders
$folders = @("Hotel", "Payment", "Review", "Room", "RoomType", "Employee", "Customer", "Booking")

foreach ($folder in $folders) {
    $files = Get-ChildItem "$basePath\$folder\*.cs"
    foreach ($file in $files) {
        Update-Namespace $file.FullName "Hotel.Booking.Application.Command.$folder"
    }
}

Write-Host "`nAll command file namespaces updated!"
