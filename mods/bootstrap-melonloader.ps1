$Folder = "MelonLoader"

if (Test-Path -Path $Folder) {
    Get-ChildItem -Path $Folder -Recurse | Remove-Item -force -recurse
    Remove-Item $Folder -force
}

mkdir -p $Folder
Set-Location $Folder
gh release -R LavaGang/MelonLoader download $MELONLOADER_VERSION -p "*.x64.zip"
Expand-Archive -LiteralPath MelonLoader.x64.zip -DestinationPath MelonLoader.x64
