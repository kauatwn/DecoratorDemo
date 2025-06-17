# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Decorator_Demo.API/Decorator_Demo.API.csproj", "src/Decorator_Demo.API/"]
COPY ["src/Decorator_Demo.Application/Decorator_Demo.Application.csproj", "src/Decorator_Demo.Application/"]
COPY ["src/Decorator_Demo.Domain/Decorator_Demo.Domain.csproj", "src/Decorator_Demo.Domain/"]
COPY ["src/Decorator_Demo.Infrastructure/Decorator_Demo.Infrastructure.csproj", "src/Decorator_Demo.Infrastructure/"]
RUN dotnet restore "./src/Decorator_Demo.API/Decorator_Demo.API.csproj"
COPY . .
WORKDIR "/src/src/Decorator_Demo.API"
RUN dotnet build "./Decorator_Demo.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Decorator_Demo.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Decorator_Demo.API.dll"]