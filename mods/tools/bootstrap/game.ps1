$gameid = 661130
$folder = "ChilloutVR"

if ($null -eq $env:STEAM_USER) {
    Write-Output "The STEAM_USER environment variable is not set. Cannot download the game."
    exit 1
}

if ($null -eq $env:STEAM_PASSWORD) {
    Write-Output "The STEAM_PASSWORD environment variable is not set. Cannot download the game."
    exit 2
}

Write-Output "Installing ChilloutVR..."

.\SteamCMD\steamcmd\steamcmd.exe `
    +@sSteamCmdForcePlatformType windows `
    +force_install_dir ../../$folder `
    +login $env:STEAM_USER "$env:STEAM_PASSWORD" `
    +app_update $gameid validate `
    +quit `
    || & { "SteamCMD exited with exit code $global:LASTEXITCODE, but we don't care about it."; $global:LASTEXITCODE = 0 }

Write-Output "Done."
Write-Output "Installing Melonloader into ChilloutVR..."

Copy-Item -Force -Recurse .\MelonLoader\MelonLoader.x64\* .\ChilloutVR\
# Copy-Item -Force .\MelonLoader\MelonLoader.x64\version.dll .\ChilloutVR\version.dll
if (!(Test-Path -Path $folder)) {
    Write-Output "Mods folder doesn't exist, creating..."
    mkdir -p .\ChilloutVR\Mods
}

# This folder doesn't exist during a ci build
if (Test-Path -Path "Mono") {
    Write-Output "Converting retail build into debug build..."

    Copy-Item -Force -Recurse "Mono\MonoExtract\Editor\Data\PlaybackEngines\windowsstandalonesupport\Variations\win64_development_mono\Data\*" ".\chilloutvr\ChilloutVR_Data\"
    Copy-Item -Force ".\Mono\MonoExtract\Editor\Data\PlaybackEngines\windowsstandalonesupport\Variations\win64_development_mono\UnityPlayer.dll" ".\chilloutvr\UnityPlayer.dll"
    Copy-Item -Force ".\Mono\MonoExtract\Editor\Data\PlaybackEngines\windowsstandalonesupport\Variations\win64_development_mono\WindowsPlayer.exe" ".\chilloutvr\ChilloutVR.exe"
    Copy-Item -Force ".\Mono\MonoExtract\Editor\Data\PlaybackEngines\windowsstandalonesupport\Variations\win64_development_mono\WinPixEventRuntime.dll" ".\chilloutvr\WinPixEventRuntime.dll"
    Copy-Item -Force ".\Mono\MonoExtract\Editor\Data\PlaybackEngines\windowsstandalonesupport\Variations\win64_development_mono\MonoBleedingEdge\EmbedRuntime\mono-2.0-bdwgc.dll" ".\chilloutvr\MonoBleedingEdge\EmbedRuntime\mono-2.0-bdwgc.dll"
}

Write-Output "Done."
