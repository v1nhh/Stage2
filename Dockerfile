FROM mcr.microsoft.com/dotnet/sdk:6.0-jammy AS build
WORKDIR /app

ARG Configuration=Release
ARG UseCTAMSharedLibraryLocally=true

# Set environment variables
ENV Configuration=${Configuration}
ENV UseCTAMSharedLibraryLocally=${UseCTAMSharedLibraryLocally}

# copy nuget config, csproj and restore as distinct layers
COPY *.sln ./
COPY CabinetModule/*.csproj ./CabinetModule/
COPY CommunicationModule/*.csproj ./CommunicationModule/
COPY ItemCabinetModule/*.csproj ./ItemCabinetModule/
COPY ItemModule/*.csproj ./ItemModule/
COPY MileageModule/*.csproj ./MileageModule/
COPY ReservationModule/*.csproj ./ReservationModule/
COPY UserRoleModule/*.csproj ./UserRoleModule/
COPY CloudAPI/*.csproj ./CloudAPI/
COPY CTAM.Core/*.csproj ./CTAM.Core/
COPY Tests/CloudAPI.Tests/*.csproj ./Tests/CloudAPI.Tests/
COPY CTAMFunctions/*.csproj ./CTAMFunctions/
COPY CTAMSeeder/*.csproj ./CTAMSeeder/
COPY CTAMCron/*.csproj ./CTAMCron/
COPY CTAMSharedLibrary/SharedLibrary/*.csproj ./CTAMSharedLibrary/SharedLibrary/

RUN dotnet restore

# copy everything else and build app
COPY ./CabinetModule ./CabinetModule/
COPY ./CommunicationModule ./CommunicationModule/
COPY ./ItemCabinetModule ./ItemCabinetModule/
COPY ./ItemModule ./ItemModule/
COPY ./MileageModule ./MileageModule/
COPY ./ReservationModule ./ReservationModule/
COPY ./UserRoleModule/ ./UserRoleModule/
COPY ./CloudAPI ./CloudAPI/
COPY ./CTAM.Core ./CTAM.Core/
COPY ./Tests/CloudAPI.Tests ./Tests/CloudAPI.Tests/
COPY ./CTAMFunctions ./CTAMFunctions/
COPY ./CTAMSeeder ./CTAMSeeder/
COPY ./CTAMCron ./CTAMCron/
COPY ./CTAMSharedLibrary/SharedLibrary ./CTAMSharedLibrary/SharedLibrary/
COPY ./scripts/wait-for-it.sh ./
WORKDIR /app/CloudAPI
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-jammy AS runtime
WORKDIR /app
COPY --from=build /app/CloudAPI/out ./ 
# This scripts ensures cloudapi is only started after database 
COPY --from=build /app/wait-for-it.sh ./
RUN chmod +x wait-for-it.sh 

ENTRYPOINT ["dotnet", "CloudAPI.dll"] 

