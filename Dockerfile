FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY WorkFlow/Server/*.csproj ./WorkFlow/Server/
COPY WorkFlow/Client/*.csproj ./WorkFlow/Client/
COPY WorkFlow/Shared/*.csproj ./WorkFlow/Shared/
RUN dotnet restore

# copy everything else and build app
COPY WorkFlow WorkFlow
WORKDIR /source/WorkFlow/Server
RUN rm appsettings.json && mv appsettings_docker.json appsettings.json
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
RUN apt-get update -y && apt-get install curl nginx -y
COPY --from=build /app ./

# HTTPs Hack (DO NOT TRY THIS AT HOME)
RUN openssl req -new -newkey rsa:4096 -days 365 -nodes -x509 -subj "/C=US/ST=Denial/L=Springfield/O=Dis/CN=localhost" -keyout localhost.key  -out localhost.crt
RUN openssl pkcs12 -export -out workflow.pfx -inkey localhost.key -in localhost.crt -password pass:
RUN cp localhost.crt /usr/local/share/ca-certificates/
RUN update-ca-certificates
COPY nginx.conf /etc/nginx/sites-available/default

ENV ASPNETCORE_URLS="https://0.0.0.0:444;http://0.0.0.0:81"
ENV ASPNETCORE_HTTPS_PORT=8001
ENV ASPNETCORE_ENVIRONMENT=Development

# Download the database and run the app
COPY docker-entrypoint.sh docker-entrypoint.sh
RUN ["chmod", "+x", "/app/docker-entrypoint.sh"]
ENTRYPOINT ["/app/docker-entrypoint.sh"]