FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 80
EXPOSE 443
EXPOSE 8080 
EXPOSE 8081
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API_EF/API_EF.csproj", "API_EF/"]
COPY ["Dto/API_Dto.csproj", "Dto/"]
COPY ["EF_Entities/EF_Entities.csproj", "EF_Entities/"]
COPY ["Entities2Dto/Entities2Dto.csproj", "Entities2Dto/"]
COPY ["EF_StubbedContextLib/EF_StubbedContextLib.csproj", "EF_StubbedContextLib/"]
COPY ["DbContextLib/EF_DbContextLib.csproj", "DbContextLib/"]
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["EventLogs/EventLogs.csproj", "EventLogs/"]
RUN dotnet restore "API_EF/API_EF.csproj"
COPY . .
WORKDIR "/src/API_EF"
RUN dotnet build "API_EF.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "API_EF.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API_EF.dll"]
