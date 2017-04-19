if defined APPVEYOR_REPO_TAG_NAME (
    forfiles /p src /m *.csproj /s /c "cmd /c dotnet pack @file -c Release"
) else (
    forfiles /p src /m *.csproj /s /c "cmd /c dotnet pack @file --version-suffix ci-%APPVEYOR_BUILD_NUMBER%"
)