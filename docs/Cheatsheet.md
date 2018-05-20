# Cheetsheet

**CAUTION**: The docker images relied upon can be quite large. It is suggested that you download these images using the appropriate data plan.

## React App

### Install
```bash
docker run --rm -it -v $(pwd):/app/ -w /app/ node:10-alpine npm install
```

### Watch
```bash
docker run --rm -it -v $(pwd):/app/ -w /app/ node:10-alpine npm run watch
```

## AspNet

### Starting watch
```bash
docker run --rm -it -p 5000:80 -v $(pwd):/app/ -w /app/ microsoft/dotnet:2.1-sdk dotnet watch run
```

## Docker

### Remove all containers

### Remove all images
USE WITH CAUTION
