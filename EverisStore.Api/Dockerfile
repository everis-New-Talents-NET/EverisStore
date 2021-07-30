#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EverisStore.Api/EverisStore.Api.csproj", "EverisStore.Api/"]
RUN dotnet restore "EverisStore.Api/EverisStore.Api.csproj"
COPY . .
WORKDIR "/src/EverisStore.Api"
RUN dotnet build "EverisStore.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EverisStore.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EverisStore.Api.dll"]