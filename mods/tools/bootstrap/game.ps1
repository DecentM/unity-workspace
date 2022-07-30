$ErrorActionPreference = 'SilentlyContinue'

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

.\SteamCMD\steamcmd\steamcmd.exe `
    +@sSteamCmdForcePlatformType windows `
    +@NoPromptForPassword 1 `
    +force_install_dir ../../$folder `
    +app_update $gameid validate `
    +quit
