#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

ARG Configuration=Release
ARG UseCTAMSharedLibraryLocally=true

ENV Configuration=${Configuration}
ENV UseCTAMSharedLibraryLocally=${UseCTAMSharedLibraryLocally}

WORKDIR /src
COPY ["CTAMCron/CTAMCron.csproj", "CTAMCron/"]
RUN dotnet restore "CTAMCron/CTAMCron.csproj"
COPY . .
WORKDIR "/src/CTAMCron"
RUN dotnet build "CTAMCron.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CTAMCron.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CTAMCron.dll"]