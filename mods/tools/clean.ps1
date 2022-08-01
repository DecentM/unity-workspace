$delfolders = ".\bin .\ChilloutVR .\MelonLoader .\SteamCMD .\NuGet .\7-Zip"

Write-Output "Recursively deleting: $delfolders"
Remove-Item -Force -Recurse $delfolders
Write-Output "Done. Run .\tools\bootstrap_all.ps1 to now do a clean installation!"
