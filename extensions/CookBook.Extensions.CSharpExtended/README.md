# CookBook Extensions AspNetCore

## About

CookBook Extensions AspNetCore is a NuGet package which adds extensions for developing of ASP.NET CORE applications.

## Pack&Publish CookBook Extensions AspNetCore package

Ensure a standalone Docker container of `CookBook Nuget Repository` server is running.

From the `build` folder run the following command to a create nuget package file:

```Bash
01_PackNuget.bat
```

After that run the following command to publish the nuget package file to the CookBook Nuget Repository server:

```Bash
02_PublishNuget.bat
```
