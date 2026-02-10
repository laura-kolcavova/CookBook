# CookBook

An example of a recipe management system built using microservices architecture

## Links

### CookBook Backend Services

[CookBook Recipes](CookBook.Recipes/README.md)

[CookBook Identity Provider](CookBook.IdentityProvider/README.md)

### CookBook Frontend Services

[CookBook Recipes Webapp](CookBook.RecipesWebapp/README.md)

### Other

[CookBook MsSql Server](CookBook.MsSqlServer/README.md)

[CookBook Nuget Repository](CookBook.NugetRepository/README.md)

[CookBook CSharpExtended NuGet Package](extensions/CookBook.Extensions.CSharpExtended/README.md)

[CookBook AspNetCore NuGet Package](extensions/CookBook.Extensions.AspNetCore/README.md)

## Deployment

1. Deploy [CookBook MsSql Server](CookBook.MsSqlServer/README.md) container:

From the `deploy` folder run the following command:

```Bash
01_MsSqlServer_Deploy.bat
```

2. Deploy [CookBook Nuget Repository](CookBook.NugetRepository/README.md) container:

From the `deploy` folder run the following command:

```Bash
02_NugetRepository_Deploy.bat
```

3. Deploy nuget extensions packages:

From the `deploy` folder run the following command:

```Bash
03_Nugets_Deploy.bat
```

4. Deploy [CookBook Recipes](CookBook.Recipes/README.md) container:

From the `deploy` folder run the following command:

```Bash
04_Recipes_Deploy.bat
```

5. Deploy [CookBook Identity Provider](CookBook.IdentityProvider/README.md) container:

From the `deploy` folder run the following command:

```Bash
05_IdentityProvider_Deploy.bat
```

6. Deploy [CookBook Recipes Webapp](CookBook.RecipesWebapp/README.md) container:

From the `deploy` folder run the following command:

```Bash
06_RecipesWebapp_Deploy.bat
```
