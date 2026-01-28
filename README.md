# About

## CookBook MsSQL Server

[MsSql Server](CookBook.MsSqlServer/README.md)

## CookBook Nuget Repository

[Nuget Repository](CookBook.NugetRepository/README.md)

## CookBook Nuget packages

[Extensions CSharpExtended](extensions/CookBook.Extensions.CSharpExtended/README.md)

[Extensions AspNetCore](extensions/CookBook.Extensions.AspNetCore/README.md)

## CookBook Backend Services

[Recipes](CookBook.Recipes/README.md)

## CookBook Frontend Services

[Recipes Webapp](CookBook.RecipesWebapp/README.md)

# Deploy

1. Deploy [MsSql Server](CookBook.MsSqlServer/README.md) container:

From the `deploy` folder run the following command:

```Bash
01_MsSqlServer_Deploy.bat
```

2. Deploy [Nuget Repository](CookBook.NugetRepository/README.md) container:

From the `deploy` folder run the following command:

```Bash
02_NugetRepository_Deploy.bat
```

3. Deploy nuget extensions packages:

From the `deploy` folder run the following command:

```Bash
03_Nugets_Deploy.bat
```

4. Deploy [Recipes](CookBook.Recipes/README.md) container:

From the `deploy` folder run the following command:

```Bash
04_Recipes_Deploy.bat
```

4. Deploy [Recipes](CookBook.RecipesWebapp/README.md) container:

From the `deploy` folder run the following command:

```Bash
05_RecipesWebapp_Deploy.bat
```
