# CookBook Extensions Configuration

## About

CookBook Extensions Configuration is a NuGet package which adds extensions for configuring of .NET Core applications.

## Pack&Publish CookBook Extensions Configuration package

Ensure a standalone Docker container of `CookBook Nuget Repository` server is running.

From the `build` folder run the following command to a create nuget package file:

```Bash
01_PackNuget.bat
```

After that run the following command to publish the nuget package file to the CookBook Nuget Repository server:

```Bash
02_PublishNuget.bat
```
