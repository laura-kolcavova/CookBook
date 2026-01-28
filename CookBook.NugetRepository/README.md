# CookBook Nuget Repository

## About

CookBook Nuget Repository is a private NuGet feed server based on [BaGet server](https://loic-sharma.github.io/BaGet/) implementation.

## Prerequisites

Ensure [Docker Desktop](https://www.docker.com/) is installed and open on your computer.

## Deployment

### Build

Run the following command to pull the BaGet server Docker image:

```Bash
docker pull loicsharma/baget
```

Or from the `deploy` folder run the following command:

```Bash
01_Container_Build.bat
```

### Release

Run the following command to start a new Docker container using the `loicsharma/baget image`:

```Bash
docker run --rm --name cookbook-nuget-repository -p 8005:80 --env-file "./src/CookBook.NugetRepository.Server/baget.env" -v "./baget-data:/var/baget" -d loicsharma/baget:latest
```

Or from the `deploy` folder run the following command:

```Bash
02_Container_Release.bat
```

The CookBook Nuget Repository server will be hosted on http://localhost:8005
