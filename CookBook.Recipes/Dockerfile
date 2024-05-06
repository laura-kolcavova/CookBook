FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY ./CookBook.Recipes.sln ./
COPY ./NuGet.Config ./
COPY ./global.json ./
COPY ./Directory.Build.props ./

COPY ./src/CookBook.Recipes.Api/*.csproj ./src/CookBook.Recipes.Api/
COPY ./src/CookBook.Recipes.Application/*.csproj ./src/CookBook.Recipes.Application/
COPY ./src/CookBook.Recipes.Constants/*.csproj ./src/CookBook.Recipes.Constants/
COPY ./src/CookBook.Recipes.Domain/*.csproj ./src/CookBook.Recipes.Domain/
COPY ./src/CookBook.Recipes.Persistence/*.csproj ./src/CookBook.Recipes.Persistence/

# restore dependencies
RUN dotnet restore ./src/CookBook.Recipes.Api/ -v diag -r linux-x64

# Copy sources and build
COPY ./src ./src/

# build
RUN dotnet publish ./src/CookBook.Recipes.Api/CookBook.Recipes.Api.csproj -v diag -c Release --no-restore -o out -r linux-x64 --self-contained false /p:PublishSingleFile=true

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
