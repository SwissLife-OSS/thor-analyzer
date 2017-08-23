dotnet restore .\src\EventSourceAnalyzer.sln

# Replaces version strings
$infos = Get-ChildItem -Path *.csproj -Recurse -Force
foreach($info in $infos)
{
    (Get-Content $info.FullName).Replace('Version>0.0.0', 'Version>' + $env:APPVEYOR_REPO_TAG_NAME) | Set-Content $info.FullName
}

dotnet build .\src\EventSourceAnalyzer.sln -c $env:CONFIGURATION