FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build-env
WORKDIR /app

COPY ./CookBook.IdentityProvider.sln ./
COPY ./NuGet.Config ./
COPY ./global.json ./
COPY ./Directory.Build.props ./

COPY ./src/CookBook.IdentityProvider.Domain/*.csproj ./src/CookBook.IdentityProvider.Domain/
COPY ./src/CookBook.IdentityProvider.Application/*.csproj ./src/CookBook.IdentityProvider.Application/
COPY ./src/CookBook.IdentityProvider.Infrastructure/*.csproj ./src/CookBook.IdentityProvider.Infrastructure/
COPY ./src/CookBook.IdentityProvider.Api/*.csproj ./src/CookBook.IdentityProvider.Api/

# Restore dependencies
RUN dotnet restore ./src/CookBook.IdentityProvider.Api/ -v diag -r linux-x64

# Copy sources and build
COPY ./src ./src/

# Build
RUN dotnet publish ./src/CookBook.IdentityProvider.Api/CookBook.IdentityProvider.Api.csproj -v diag -c Release --no-restore -o out -r linux-x64 --self-contained false /p:PublishSingleFile=true

# Create runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim
WORKDIR /app

ENV TZ Europe/Prague
EXPOSE 8020
ENV ASPNETCORE_URLS=http://*:8020

# Required to enable read only root filesystem
ENV DOTNET_EnableDiagnostics=0

RUN addgroup --gid 2000 dotnetappgroup && adduser --uid 1000 dotnetappuser --gid 2000

RUN chown 1000:2000 /app
USER 1000:2000

COPY --from=build-env /app/out .

ENTRYPOINT ["./CookBook.IdentityProvider.Api"]
