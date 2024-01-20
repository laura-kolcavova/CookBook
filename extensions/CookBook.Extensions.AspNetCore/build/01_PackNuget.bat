dotnet pack "..\src\CookBook.Extensions.AspNetCore.csproj" -c Release
dotnet nuget push "..\src\bin\Release\*.nupkg" --source CookBookPackages