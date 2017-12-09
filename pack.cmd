@echo off
if not defined APPVEYOR_REPO_TAG_NAME (
    echo Packaging ci version
    forfiles /p src /m *.csproj /s /c "cmd /c dotnet pack @file --version-suffix ci-%APPVEYOR_BUILD_NUMBER%"
) else if "%APPVEYOR_REPO_TAG_NAME%" == "%APPVEYOR_REPO_TAG_NAME:*-=%" (
    echo Packaging release version
    forfiles /p src /m *.csproj /s /c "cmd /c dotnet pack @file -c Release"
) else (
    echo Packaging prerelease %APPVEYOR_REPO_TAG_NAME:*-=%
    forfiles /p src /m *.csproj /s /c "cmd /c dotnet pack @file -c Release --version-suffix %APPVEYOR_REPO_TAG_NAME:*-=%"
)
