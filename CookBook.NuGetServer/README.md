# CookBook Nuget Repository

CookBook Nuget Repository is a private NuGet feed server based on [BaGet server](https://loic-sharma.github.io/BaGet/) implementation.

## Prerequisites

Ensure [Docker Desktop](https://www.docker.com/) is installed and open on your computer.

## Deployment

### Build

Run the following command to pull the BaGet server Docker image:

```Bash
docker build -t cookbook-nuget-server-image .
```

Or from the `deploy` folder run the following command:

```Bash
01_Image_Build.bat
```

### Release

Run the following command to start a new Docker container using the `loicsharma/baget image`:

```Bash
docker run -it --rm -d -p 8005:80 --name cookbook-nuget-server-container --network cookbook-network --env-file "./baget.env" -v "baget-data:/var/baget" cookbook-nuget-server-image
```

Or from the `deploy` folder run the following command:

```Bash
02_Container_Release.bat
```

The CookBook Nuget Repository server will be hosted on http://localhost:8005
