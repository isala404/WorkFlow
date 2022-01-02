FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY WorkFlow WorkFlow
WORKDIR /src/WorkFlow/Server
RUN dotnet restore "WorkFlow.Server.csproj"
RUN dotnet build "WorkFlow.Server.csproj" -c Release -o /app/build
RUN dotnet publish "WorkFlow.Server.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update -y && apt-get install curl -y
COPY --from=build /app/publish .
COPY docker-entrypoint.sh docker-entrypoint.sh
RUN ["chmod", "+x", "/app/docker-entrypoint.sh"]
ENTRYPOINT ["/app/docker-entrypoint.sh"]