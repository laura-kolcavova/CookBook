### Client
FROM node:20.13.1 AS react-client-build
WORKDIR /app
COPY ./client .
RUN npm ci
RUN npm run build:prod
# RUN sed -i "s/VERSION/$CI_COMMIT_TAG/g" build/index.html

FROM mcr.microsoft.com/dotnet/sdk:8.0-noble AS build-env
WORKDIR /app

COPY ./CookBook.RecipesWebapp.Server.sln ./
COPY ./NuGet.Config ./
COPY ./global.json ./
COPY ./Directory.Build.props ./

COPY ./server/src/CookBook.RecipesWebapp.Server.Api/CookBook.RecipesWebapp.Server.Api.csproj ./server/src/CookBook.RecipesWebapp.Server.Api/

# Restore dependencies
RUN dotnet restore ./server/src/CookBook.RecipesWebapp.Server.Api/ -v diag -r linux-x64

# Copy sources and build
COPY ./server/src ./server/src/

# Build
RUN dotnet publish ./server/src/CookBook.RecipesWebapp.Server.Api/CookBook.RecipesWebapp.Server.Api.csproj -v diag -c Release --no-restore -o out -r linux-x64 --self-contained false /p:PublishSingleFile=true

# Create runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-noble-chiseled-extra
WORKDIR /app

ENV TZ Europe/Prague
EXPOSE 8015
ENV ASPNETCORE_URLS=http://*:8015

# Required to enable read only root filesystem
ENV DOTNET_EnableDiagnostics=0

RUN addgroup --gid 2000 dotnetappgroup && adduser --uid 1000 dotnetappuser --gid 2000

RUN chown 1000:2000 /app
USER 1000:2000

COPY --from=build-env /app/out .
# COPY --from=react-client-build /build /app/client/build
COPY --from=react-client-build /app/build /app/client/build

ENTRYPOINT ["./CookBook.RecipesWebapp.Server.Api"]
