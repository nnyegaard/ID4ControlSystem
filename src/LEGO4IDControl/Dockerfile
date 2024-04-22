FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY "LEGO4IDControl.csproj" .
#COPY ["src/LEGO4IDControlTest1.csproj", "LEGO4IDControlTest1/"]
RUN dotnet restore "LEGO4IDControl.csproj"

COPY . .
RUN dotnet build "LEGO4IDControl.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LEGO4IDControl.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LEGO4IDControl.dll"]
