FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY ../nuget ./nuget/
COPY ./NuGet.Config ./
# COPY ./global.json ./
# COPY ./Directory.Build.props ./

COPY ./CookBook.Recipes.sln ./

COPY ./src/CookBook.Recipes.Api/CookBook.Recipes.Api.csproj ./src/CookBook.Recipes.Api/

# restore dependencies
RUN dotnet restore ./src/CookBook.Recipes.Api/ -r linux-x64

# Copy sources and build
COPY ./src ./src/

# build
# "GenerateCode=false" is to disable API Client generation
RUN dotnet publish ./src/CookBook.Recipes.Api/CookBook.Recipes.Api.csproj -c Release --no-restore -o out -r linux-x64 --self-contained false /p:PublishSingleFile=true /p:GenerateCode=false

# Create runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

ENV TZ Europe/Prague
EXPOSE 8010
ENV ASPNETCORE_URLS=http://*:8010
# required to enable read only root filesystem
ENV COMPlus_EnableDiagnostics=0

RUN addgroup --gid 2000 dotnetappgroup && adduser --uid 1000 dotnetappuser --gid 2000

RUN chown 1000:2000 /app
USER 1000:2000

COPY --from=build-env /app/out .
ENTRYPOINT ["./CookBook.Recipes.Api"]
