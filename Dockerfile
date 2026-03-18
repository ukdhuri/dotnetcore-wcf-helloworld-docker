# Use the official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["HelloWorldWcf.csproj", "./"]
RUN dotnet restore "HelloWorldWcf.csproj"

# Copy the rest of the code and build
COPY . .
RUN dotnet build "HelloWorldWcf.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "HelloWorldWcf.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the official .NET 8 ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose port 8080 which is default for ASP.NET Core 8.0
EXPOSE 8080

ENTRYPOINT ["dotnet", "HelloWorldWcf.dll"]
