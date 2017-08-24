if (Test-Path env:APPVEYOR)
{
    if (Test-Path env:PRBUILD)
    {
        .\build\runtests.ps1
    }
    else
    {
        Invoke-Expression ((Get-ChildItem ($env:USERPROFILE + '\.nuget\packages\OpenCover'))[0].FullName + '\tools\OpenCover.Console.exe -register:user -target:".\build\runtests.ps1" -searchdirs:".\src\*\bin\' + $env:CONFIGURATION + '\netcoreapp2.0" -oldstyle -output:coverage.xml -skipautoprops -returntargetcode -filter:"+[*Analyzer]*"')
        Invoke-Expression ((Get-ChildItem ($env:USERPROFILE + '\.nuget\packages\coveralls.io'))[0].FullName + '\tools\coveralls.net.exe --opencover coverage.xml')
        dotnet pack .\src\EventSourceAnalyzer.sln --include-symbols --include-source -c $env:CONFIGURATION /p:PackageVersion=$env:CC_BUILD_VERSION
    }
}