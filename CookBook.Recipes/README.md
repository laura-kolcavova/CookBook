# CookBook Recipes

## About

CookBook Recipes is a service for managing recipes, its ingredients and instructions.

## Use of CookBook Recipes

Ensure [Docker Desktop](https://www.docker.com/) is installed and open on your computer.

Run this command to create the image and tag it with the name book-catalog:

```Bash
docker build -t cookbook-recipes .
```

To run the web API service, run the following command to start a new Docker container using the book-catalog image and expose the service on port 8010:

```Bash
docker run -it --rm -p 8010:80 --name cookbook-recipes-container cookbook-recipes
```

This service will be hosted on http://localhost:8010