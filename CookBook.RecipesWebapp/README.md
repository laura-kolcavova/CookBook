# CookBook Recipes Webapp

## About

CookBook Recipes Webapp is a frontend service for managing recipes, its ingredients and instructions.

## Architecture

The application consists of two main components:

- **React Client**: A frontend application providing an interactive user interface for managing recipes
  - React 19
  - TypeScript
  - Tailwind CSS
  - Vite
  - Axios
  - TanStack Query
  - Jotai

- **BFF (Backend for Frontend)**: A server that acts as an intermediary between the client and backend services, handling API aggregation and business logic specific to the webapp
  - .NET Core 8
  - YARP

## Prerequisites

Ensure [Docker Desktop](https://www.docker.com/) is installed and open on your computer.

Ensure a [MsSql Server ](../CookBook.MsSqlServer//README.md/) container is running.

Ensure a [Nuget Repository](../CookBook.NugetRepository/README.md/) container is running.

Ensure [nuget extensions packages](../README.md#deploy) are deployed at `Nuget Repository`.

Ensure a [Recipes](../CookBook.Recipes/README.md/) container is running.

## Deployment

### Build container

Run the following command to create a Docker image and tag it with the name cookbook-recipes-webapp (host network must be used for building the image):

```Bash
docker build -t cookbook-recipes-webapp --network host ../
```

Or from the `deploy` folder run the following command:

```Bash
01_Container_Build.bat
```

### Release container

Run the following command to start a new Docker container using the cookbook-recipes-webapp image:

```Bash
docker run -it --rm -p 8015:8015 --name cookbook-recipes-webapp-container -d cookbook-recipes-webapp
```

Or from the `deploy` folder run the following command:

```Bash
02_Container_Release.bat
```

The CookBook Recipes Webapp service will be hosted on http://localhost:8015
