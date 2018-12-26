FROM node:10-alpine AS react-app-builder
LABEL stage=intevent-web-intermediate
RUN mkdir -p /app/dist
WORKDIR /app
COPY . /app
RUN npm ci
RUN npm run build:production

FROM microsoft/dotnet:2.2-sdk-alpine AS aspnet-builder
LABEL stage=intevent-web-intermediate
RUN mkdir -p /app/dist
WORKDIR /app
COPY . /app
RUN dotnet publish -c Release -o dist

FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine
RUN mkdir -p /app/wwwroot/js
WORKDIR /app
COPY --from=aspnet-builder /app/dist .
COPY --from=react-app-builder /app/dist ./wwwroot/js

EXPOSE 80
ENTRYPOINT ["dotnet", "intevent-web.dll"]
