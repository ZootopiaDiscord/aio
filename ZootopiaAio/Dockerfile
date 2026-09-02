FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:11.0.100-preview.7 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["global.json", "./"]
COPY ["ZootopiaAio/ZootopiaAio.csproj", "ZootopiaAio/"]
COPY ["ZootopiaAio.Bot/ZootopiaAio.Bot.csproj", "ZootopiaAio.Bot/"]
COPY ["ZootopiaAio.Web/ZootopiaAio.Web.csproj", "ZootopiaAio.Web/"]
COPY ["ZootopiaAio.Web.Client/ZootopiaAio.Web.Client.csproj", "ZootopiaAio.Web.Client/"]
RUN dotnet restore "ZootopiaAio/ZootopiaAio.csproj"
COPY . .
WORKDIR "/src/ZootopiaAio"
RUN dotnet build "./ZootopiaAio.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ZootopiaAio.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZootopiaAio.dll"]
