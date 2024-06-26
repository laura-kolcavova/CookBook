# CookBook Extensions AspNetCore

## About

CookBook Extensions AspNetCore is a NuGet package which adds extensions for developing of ASP.NET CORE applications.

## Prerequisites

Ensure [Docker Desktop](https://www.docker.com/) is installed and open on your computer.

Ensure a [Nuget Repository](../../CookBook.NugetRepository/README.md/) container is running.

## Pack

Run fololowing commands to build and create a nuget package file:

```Bash
dotnet build ".\src\CookBook.Extensions.AspNetCore.csproj" --configuration Release
dotnet pack ".\src\CookBook.Extensions.AspNetCore.csproj" -c Release
```

Or from the `deploy` folder run the following command:

```Bash
01_Nuget_Pack.bat
```

## Release

Run fololowing command to publish the nuget package file to the `CookBook Nuget Repository server`:

```Bash
dotnet nuget push ".\src\bin\Release\*.nupkg" --source CookBookPackages
```

Or from the `deploy` folder run the following command:

```Bash
02_Nuget_Release.bat
```
