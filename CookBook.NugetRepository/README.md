# CookBook Nuget Repository

## About

CookBook Nuget Repository is a private NuGet feed server based on [BaGet server](https://loic-sharma.github.io/BaGet/) implementation.

## Run CookBook Nuget Repository as a standalone Docker container

Ensure [Docker Desktop](https://www.docker.com/) is installed and open on your computer.

Execute this command to pull the BaGet server Docker image:

```Bash
docker pull loicsharma/baget
```

Run the following command to start a new Docker container using the loicsharma/baget image:

```Bash
docker run --rm --name cookbook-nuget-repository -p 5555:80 --env-file "./src/CookBook.NugetRepository.Server/baget.env" -v "./baget-data:/var/baget" loicsharma/baget:latest
```

The CookBook Nuget Repository server will be hosted on http://localhost:5555