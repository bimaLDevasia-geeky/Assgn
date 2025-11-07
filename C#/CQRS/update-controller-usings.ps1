# Script to update controller using statements for new Query and Command namespaces

$controllerPath = "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.API\Controllers"

function Update-ControllerUsings {
    param([string]$filePath)
    
    $content = Get-Content $filePath -Raw
    
    # Add specific using statements for the entity-specific namespaces
    $entityName = [System.IO.Path]::GetFileNameWithoutExtension($filePath) -replace 'Controller$', ''
    
    # Remove old generic Query using if present
    $content = $content -replace 'using Hotel\.Booking\.Application\.Query;\s*[\r\n]+', ''
    $content = $content -replace 'using Hotel\.Booking\.Application\.Command;\s*[\r\n]+', ''
    
    # Add entity-specific namespaces after MediatR using
    if ($content -match 'using MediatR;') {
        $content = $content -replace '(using MediatR;)', "`$1`r`nusing Hotel.Booking.Application.Query.$entityName;`r`nusing Hotel.Booking.Application.Command.$entityName;"
    }
    
    Set-Content $filePath -Value $content -NoNewline
    Write-Host "Updated: $filePath"
}

# Process all controllers
$controllers = Get-ChildItem "$controllerPath\*Controller.cs" | Where-Object { $_.Name -notmatch 'WeatherForecast' }

foreach ($controller in $controllers) {
    Update-ControllerUsings $controller.FullName
}

Write-Host "`nAll controllers updated with new namespaces!"
