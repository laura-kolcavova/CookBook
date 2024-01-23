# CookBook Recipes

## About

CookBook Recipes is a service for managing recipes, its ingredients and instructions.

## Run CookBook Recipes service as a standalone Docker container

Ensure [Docker Desktop](https://www.docker.com/) is installed and open on your computer.

Run the following command to create a Docker image and tag it with the name book-recipes (host network must be used for building the image):

```Bash
docker build -t cookbook-recipes --network host .
```

Run the following command to start a new Docker container using the book-catalog image:

```Bash
docker run -it --rm -p 8010:8010 --name cookbook-recipes-container cookbook-recipes
```

The CookBook Recipes service will be hosted on http://localhost:8010