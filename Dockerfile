FROM node:10-alpine AS react-app-builder
LABEL stage=intevent-web-intermediate
RUN mkdir -p /app
WORKDIR /app
COPY . /app
RUN npm install
RUN npm run build:production

FROM microsoft/dotnet:2.1-sdk-alpine AS aspnet-builder
LABEL stage=intevent-web-intermediate
RUN mkdir -p /app/wwwroot/js
WORKDIR /app
COPY . /app
RUN dotnet publish -c Release -o dist

FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine
RUN mkdir -p /app
WORKDIR /app
COPY --from=aspnet-builder /app/dist .
COPY --from=react-app-builder /app/dist /wwwroot/js

EXPOSE 5000
ENTRYPOINT ["dotnet", "intevent-web.dll"]
