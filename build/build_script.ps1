# Restore packages
dotnet restore .\src\EventSourceAnalyzer.sln

# Set version
$infos = Get-ChildItem -Path *.csproj -Recurse -Force
foreach($info in $infos)
{
    (Get-Content $info.FullName).Replace("Version>0.0.0", "Version>" + $env:APPVEYOR_REPO_TAG_NAME) | Set-Content $info.FullName
}

# Build & Analyse
# Sonar for .Net Core is not supported right now; for more information go to https://jira.sonarsource.com/browse/SONARMSBRU-310
#Invoke-Expression ('SonarQube.Scanner.MSBuild.exe begin /k:"' + $env:APPVEYOR_PROJECT_NAME + '" /d:"sonar.host.url=https://sonarqube.com" /d:"sonar.login=' + $env:SONARQUBE_TOKEN + '" /v:"' + $env:APPVEYOR_REPO_TAG_NAME + '"')
dotnet build .\src\EventSourceAnalyzer.sln -c $env:CONFIGURATION
#Invoke-Expression ('SonarQube.Scanner.MSBuild.exe end /d:"sonar.login=' + $env:SONARQUBE_TOKEN + '"')