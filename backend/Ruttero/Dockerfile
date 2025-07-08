 # Stage 1
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

 # Copy files
COPY . ./

 # Restore dependencies
RUN dotnet restore

 # Publish project
RUN dotnet publish -c Release -o /out

 # Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

 # Copy published project
COPY --from=build /out ./

 # Expose default port
EXPOSE 80

 # Start command
ENTRYPOINT ["dotnet", "Ruttero.dll"]
