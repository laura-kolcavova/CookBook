dotnet pack "..\src\CookBook.Extensions.Configuration.csproj" -c Release
dotnet nuget push "..\src\bin\Release\*.nupkg" --source CookBookPackages