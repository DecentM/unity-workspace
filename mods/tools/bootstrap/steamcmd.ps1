$url = "https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip"
$folder = "SteamCMD"

if (Test-Path -Path $folder) {
    Get-ChildItem -Path $folder -Recurse | Remove-Item -force -recurse
    Remove-Item $folder -force
}

mkdir -p $folder
$pastlocation = Get-Location
Set-Location $folder

Invoke-WebRequest $url -OutFile steamcmd.zip
Expand-Archive -LiteralPath steamcmd.zip -DestinationPath steamcmd

Set-Location $pastlocation
