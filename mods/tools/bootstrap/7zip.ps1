return "7-Zip should be installed system-wide. 7zr cannot extract the Unity Installer. Skipping...";

$url = "https://www.7-zip.org/a/7zr.exe"
$folder = "7-Zip"

if (Test-Path -Path $folder) {
    return "Skipping 7-Zip install as it's already installed. Run '\tools\clean.ps1' to clear the current installation!";
}

Write-Output "Installing 7-Zip..."

mkdir -p $folder
$pastlocation = Get-Location
Set-Location $folder

Invoke-WebRequest $url -OutFile "7z.exe"
# .\7zip-setup.exe /S /D=".\7-Zip"

Set-Location $pastlocation

Write-Output "Done."
