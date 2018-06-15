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

### Install
```bash
docker run --rm -it -p 5000:80 -v $(pwd):/app/ -w /app/ microsoft/dotnet:2.1-sdk-alpine dotnet restore
```

### Watch
```bash
export ASPNETCORE_ENVIRONMENT=Development
docker run --rm -it -p 5000:80 -v $(pwd):/app/ -w /app/ microsoft/dotnet:2.1-sdk-alpine dotnet watch run
```

## Docker

### Building Production image
**CAUTION**: Running this requires 3 separate docker images, and will re-download all dotnet and npm packages
```bash
docker build --no-cache -t intevent-web .
```

### Remove all stopped containers
```bash
docker rm $(docker ps -a -q)
```

### Remove all images
**USE WITH CAUTION**
```bash
docker rmi $(docker images -q)
```
