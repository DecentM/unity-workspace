$gameid = 661130
$folder = "ChilloutVR"

.\SteamCMD\steamcmd\steamcmd.exe `
    +@sSteamCmdForcePlatformType windows `
    +force_install_dir ../../$folder `
    +app_update $gameid validate `
    +quit
