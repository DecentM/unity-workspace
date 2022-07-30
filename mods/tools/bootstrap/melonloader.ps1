$folder = "MelonLoader"

if (Test-Path -Path $folder) {
    Get-ChildItem -Path $folder -Recurse | Remove-Item -force -recurse
    Remove-Item $folder -force
}

mkdir -p $folder
$pastlocation = Get-Location
Set-Location $folder

gh release -R LavaGang/MelonLoader download $MELONLOADER_VERSION -p "*.x64.zip"
Expand-Archive -LiteralPath MelonLoader.x64.zip -DestinationPath MelonLoader.x64

Set-Location $pastlocation
