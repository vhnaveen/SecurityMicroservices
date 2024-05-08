#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SecurityMicroservice_A/SecurityMicroservice_A.csproj", "SecurityMicroservice_A/"]
RUN dotnet restore "./SecurityMicroservice_A/SecurityMicroservice_A.csproj"
COPY . .
WORKDIR "/src/SecurityMicroservice_A"
RUN dotnet build "./SecurityMicroservice_A.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SecurityMicroservice_A.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecurityMicroservice_A.dll"]