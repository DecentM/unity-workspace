$gameid = 661130

if ($null -eq $env:STEAM_USER) {
    Write-Output "The STEAM_USER environment variable is not set. Cannot download the game."
    exit 1
}

if ($null -eq $env:STEAM_PASSWORD) {
    Write-Output "The STEAM_PASSWORD environment variable is not set. Cannot download the game."
    exit 2
}

.\SteamCMD\steamcmd\steamcmd.exe `
    +@ShutdownOnFailedCommand 1 `
    +@NoPromptForPassword 1 `
    +force_install_dir ../../ChilloutVR `
    +login $env:STEAM_USER $env:STEAM_PASSWORD `
    +app_update $gameid validate `
    +quit
