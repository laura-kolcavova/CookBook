# CookBook MsSql Server

CookBook MsSql Server is a private SQL server running via [Microsoft SQL server - Ubuntu image](https://github.com/Microsoft/mssql-docker).

## Prerequisites

Ensure [Docker Desktop](https://www.docker.com/) is installed and open on your computer.

## Deployment

### Build

Run the following command to pull the Microsoft SQL server Docker image:

```Bash
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

Or from the `deploy` folder run the following command:

```Bash
01_Container_Build.bat
```

### Release

Run the following command to start a new Docker container using the Microsoft SQL server Docker image:

```Bash
docker run --rm --name cookbook-mssql-server -p 8000:1433 --env-file "./src/CookBook.MsSqlServer.Server/server.env" -d mcr.microsoft.com/mssql/server:2022-latest
```

Or from the `deploy` folder run the following command:

```Bash
02_Container_Release.bat
```

The CookBook MsSql Server will be hosted at address `localhost,8000` or `host.docker.internal,8000`
