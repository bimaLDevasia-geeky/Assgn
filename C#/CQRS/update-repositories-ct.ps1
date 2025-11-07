# Script to add CancellationToken to all repository interfaces and implementations

# Update repository interfaces
$interfacePath = "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Domain\Interfaces"
$repoPath = "C:\Users\Bimal.Devasia\Desktop\backend\C#\CQRS\Hotel.Booking.Infrastructure\Repository"

function Update-RepositoryInterface {
    param([string]$filePath)
    
    $content = Get-Content $filePath -Raw
    
    # Add CancellationToken to GetByIdAsync if not already present
    if ($content -notmatch 'GetByIdAsync\(Guid.*?,\s*CancellationToken') {
        $content = $content -replace 'Task<([^>]+)> GetByIdAsync\(Guid ([^)]+)\);', 'Task<$1> GetByIdAsync(Guid $2, CancellationToken ct);'
    }
    
    # Add CancellationToken to GetAllAsync if not already present
    if ($content -notmatch 'GetAllAsync\(CancellationToken') {
        $content = $content -replace 'Task<([^>]+)> GetAllAsync\(\);', 'Task<$1> GetAllAsync(CancellationToken ct);'
    }
    
    Set-Content $filePath -Value $content -NoNewline
    Write-Host "Updated interface: $filePath"
}

function Update-RepositoryImpl {
    param([string]$filePath)
    
    $content = Get-Content $filePath -Raw
    
    # Add CancellationToken to GetByIdAsync if not already present (keep default parameter)
    if ($content -notmatch 'GetByIdAsync\(Guid.*?,\s*CancellationToken') {
        $content = $content -replace 'public async Task<([^>]+)> GetByIdAsync\(Guid ([^)]+)\)', 'public async Task<$1> GetByIdAsync(Guid $2, CancellationToken ct = default)'
    }
    
    # Update FindAsync calls to pass CancellationToken
    $content = $content -replace 'await _context\.(\w+)\.FindAsync\((\w+)\);', 'return await _context.$1.FindAsync(new object[] { $2 }, ct);'
    
    # Add CancellationToken to GetAllAsync if not already present
    if ($content -notmatch 'GetAllAsync\(CancellationToken') {
        $content = $content -replace 'public async Task<([^>]+)> GetAllAsync\(\)', 'public async Task<$1> GetAllAsync(CancellationToken ct = default)'
    }
    
    # Update ToListAsync calls to pass CancellationToken  
    $content = $content -replace 'await _context\.(\w+)\.ToListAsync\(\);', 'return await _context.$1.ToListAsync(ct);'
    
    Set-Content $filePath -Value $content -NoNewline
    Write-Host "Updated implementation: $filePath"
}

# Process all repository interfaces (exclude IHotelRepository as it's already updated)
$interfaces = Get-ChildItem "$interfacePath\I*Repository.cs" | Where-Object { $_.Name -ne "IHotelRepository.cs" }
foreach ($file in $interfaces) {
    Update-RepositoryInterface $file.FullName
}

# Process all repository implementations (exclude HotelRepository as it's already updated)
$implementations = Get-ChildItem "$repoPath\*Repository.cs" | Where-Object { $_.Name -ne "HotelRepository.cs" }
foreach ($file in $implementations) {
    Update-RepositoryImpl $file.FullName
}

Write-Host "`nAll repositories updated with CancellationToken!"
