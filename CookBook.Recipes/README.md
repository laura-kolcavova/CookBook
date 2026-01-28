# CookBook Recipes

## About

CookBook Recipes is a service for managing recipes, its ingredients and instructions.

## Architecture

A RESTful API backend service for managing recipes, ingredients, and cooking instructions. The service follows clean architecture principles with domain-driven design and vertical-slice architecture.

**Technologies:**

- .NET Core 8
- ASP.NET Core Minimal API
- Entity Framework Core

## Prerequisites

Ensure [Docker Desktop](https://www.docker.com/) is installed and open on your computer.

Ensure a [MsSql Server ](../CookBook.MsSqlServer//README.md/) container is running.

Ensure a [Nuget Repository](../CookBook.NugetRepository/README.md/) container is running.

Ensure [nuget extensions packages](../README.md#deploy) are deployed at `Nuget Repository`.

## Deployment

### Build database

Run the following command to build the database project:

```bash
dotnet build "./database/CookBook.Recipes.Database.sqlproj" -o "./database/build_output"
```

Or from the `deploy` folder run the following command:

```Bash
01_Database_Build.bat
```

### Database release

Run the following command to publish the database project to `MsSql Server`:

```Bash
sqlpackage /a:Publish /sf:"./database/build_output/CookBook.Recipes.Database.dacpac" /TargetConnectionString:"Data source=localhost,8000;User Id=SA;Initial Catalog=CookBookRecipes;Integrated Security=False;TrustServerCertificate=True;Application Name=CookBookRecipes;Password=y9WH7F4hNL"
```

Or from the `deploy` folder run the following command:

```Bash
02_Database_Release.bat
```

### Build container

Run the following command to create a Docker image and tag it with the name book-recipes (host network must be used for building the image):

```Bash
docker build -t cookbook-recipes --network host .
```

Or from the `deploy` folder run the following command:

```Bash
03_Container_Build.bat
```

### Release container

Run the following command to start a new Docker container using the book-catalog image:

```Bash
docker run -it --rm -p 8010:8010 --name cookbook-recipes-container -d cookbook-recipes
```

Or from the `deploy` folder run the following command:

```Bash
04_Container_Release.bat
```

The CookBook Recipes service will be hosted on http://localhost:8010
