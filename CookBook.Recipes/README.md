# CookBook Recipes

## About

CookBook Recipes is a service for managing recipes, its ingredients and instructions.

## Prerequisites

Ensure [Docker Desktop](https://www.docker.com/) is installed and open on your computer.

Ensure a [MsSql Server ](../CookBook.MsSqlServer//README.md/) container is running.

Ensure a [Cookbook Nuget Repository](../CookBook.NugetRepository/README.md/) container is running.

Ensure [nuget extensions packages](../README.md#deploy/) are deployed at `Cookbook Nuget Repository`.

## Build

Run the following command to create a Docker image and tag it with the name book-recipes (host network must be used for building the image):

```Bash
docker build -t cookbook-recipes --network host .
```

Or from the `deploy` folder run the following command:

```Bash
01_Container_Build.bat
```

## Release

Run the following command to start a new Docker container using the book-catalog image:

```Bash
docker run -it --rm -p 8010:8010 --name cookbook-recipes-container cookbook-recipes
```

Or from the `deploy` folder run the following command:

```Bash
02_Container_Release.bat
```

The CookBook Recipes service will be hosted on http://localhost:8010
