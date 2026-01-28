# CookBook

An example of a recipe management system built using microservices architecture

## Links

### Backend Services

[CookBook Recipes](CookBook.Recipes/README.md)

### Frontend Services

[CookBook Recipes Webapp](CookBook.RecipesWebapp/README.md)

### Other

[CookBook MsSql Server](CookBook.MsSqlServer/README.md)

[CookBook NuGet Repository](CookBook.NugetRepository/README.md)

[CookBook.Extensions.CSharpExtended NuGet Package](extensions/CookBook.Extensions.CSharpExtended/README.md)

[CookBook.Extensions.AspNetCore NuGet Package](extensions/CookBook.Extensions.AspNetCore/README.md)

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
