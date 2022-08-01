$unitysetup31 = "UnitySetup64-2019.4.31f1.exe"
$unitysetup28 = "UnitySetup64-2019.4.28f1.exe"
$url31 = "https://download.unity3d.com/download_unity/bd5abf232a62/Windows64EditorInstaller/$unitysetup31"
$url28 = "https://download.unity3d.com/download_unity/1381962e9d08/Windows64EditorInstaller/$unitysetup28"
$folder = "Mono"

#if (Test-Path -Path $folder) {
#    return "Skipping Mono install as it's already installed. Run '\tools\clean.ps1' to clear the current installation!";
#}

Write-Output "Installing Mono..."

if (!(Test-Path -Path $folder)) {
    mkdir -p $folder
}

$pastlocation = Get-Location
Set-Location $folder

# if (Test-Path -Path "mono") {
#     Write-Output "Updating mono..."
#     Set-Location "mono"
#     git reset --hard
#     git clean -f .
#     git fetch
#     git pull
#     git checkout unity-2019.4-mbe
#     Set-Location ".."
# } else {
#     Write-Output "Cloning mono..."
#     git clone https://github.com/Unity-Technologies/mono.git --recurse-submodules -j8
#     Set-Location mono
#     git fetch
#     git pull
#     git checkout unity-2019.4-mbe
#     Set-Location ..
# }
# 
# if (Test-Path -Path "dnSpy-Unity-mono") {
#     Write-Output "Updating dnSpy-Unity-mono..."
#     Set-Location "dnSpy-Unity-mono"
#     git reset --hard
#     git clean -f .
#     git pull
#     Set-Location ".."
# }
# else {
#     Write-Output "Cloning dnSpy-Unity-mono..."
#     git clone https://github.com/dnSpyEx/dnSpy-Unity-mono
# }

# Set-Location dnSpy-Unity-mono\src\umpatcher\
# dotnet build -c Release
# Set-Location ../../../

if (Test-Path -Path "$unitysetup31") {
    Write-Output "Found existing Unity 2019.4.31f1 installer..."
} else {
    Write-Output "Downloading Unity 2019.4.31f1 installer..."
    Invoke-WebRequest $url31 -OutFile "$unitysetup31"
}

if (Test-Path -Path "$unitysetup28") {
    Write-Output "Found existing Unity 2019.4.28f1 installer..."
}
else {
    Write-Output "Downloading Unity 2019.4.28f1 installer..."
    Invoke-WebRequest $url28 -OutFile "$unitysetup28"
}

if (Test-Path -Path "MonoExtract") {
    Remove-Item -Force -Recurse .\MonoExtract
}

# $env:PATH += ";C:\Program Files\7-Zip"; ..\tools\extractmono.bat "." "MonoExtract" "both"

$monopath = "Editor\Data\PlaybackEngines\windowsstandalonesupport\Variations\win64_development_mono"

7z x "$unitysetup28" "$monopath" -oMonoExtract

Set-Location $pastlocation

# umpatcher doesn't check if the source folders exist, so we create fake ones to prevent a crash
# if (!(Test-Path -Path ".\mono\eglib")) {
#     mkdir -p .\mono\eglib
# }
# 
# if (!(Test-Path -Path ".\mono\unity")) {
#     mkdir -p .\mono\unity
# }
# 
# $unityversion = "2019.4.31f1"
# $unitycommit = "522858ace1f4f18f1a23b78530f1b7ea1118e35c"
# 
# Copy-Item -Recurse -Force .\mono\* .\dnSpy-Unity-mono\unity-2019.4.31f1\
# .\dnSpy-Unity-mono\src\umpatcher\umpatcher\bin\Release\net48\umpatcher.exe $unityversion $unitycommit .\mono .\dnSpy-Unity-mono
# Set-Location .\dnSpy-Unity-mono\unity-$unityversion
# 
# # TODO: Retarget automatically
# Write-Output "Script paused. Open Visual Studio and retarget the application for .NET 4.8, then press Enter."
# pause
# 
# dotnet build

Write-Output "Done."
