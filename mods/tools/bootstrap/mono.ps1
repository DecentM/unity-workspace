$unitysetup = "UnitySetup64-2019.4.28f1.exe"
$url = "https://download.unity3d.com/download_unity/1381962e9d08/Windows64EditorInstaller/$unitysetup"
$folder = "Mono"

Write-Output "Installing Mono..."

if (!(Test-Path -Path $folder)) {
    mkdir -p $folder
}

$pastlocation = Get-Location
Set-Location $folder

if (Test-Path -Path "$unitysetup") {
    Write-Output "Found existing $unitysetup"
} else {
    Write-Output "Downloading $unitysetup..."
    Invoke-WebRequest $url -OutFile "$unitysetup"
}

if (Test-Path -Path "MonoExtract") {
    Remove-Item -Force -Recurse .\MonoExtract
}

$monopath = "Editor\Data\PlaybackEngines\windowsstandalonesupport\Variations\win64_development_mono"

7z x "$unitysetup" "$monopath" -oMonoExtract

Set-Location $pastlocation

Write-Output "Done."
