# CookBook MsSql Server

## About

CookBook MsSql Server is a private SQL server running via [Microsoft SQL server - Ubuntu image](https://github.com/Microsoft/mssql-docker).

## Run CookBook MsSql Server as a standalone Docker container

Ensure [Docker Desktop](https://www.docker.com/) is installed and open on your computer.

Run the following command to pull the Microsoft SQL server Docker image:

```Bash
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

Run the following command to start a new Docker container using the Microsoft SQL server Docker image:

```Bash
docker run --rm --name cookbook-mssql-server -p 8000:1433 --env-file "./src/CookBook.MsSqlServer.Server/server.env" -d mcr.microsoft.com/mssql/server:2022-latest
```

The CookBook MsSql Server will be hosted on http://localhost:8000
