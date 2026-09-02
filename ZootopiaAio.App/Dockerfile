FROM mcr.microsoft.com/dotnet/sdk:11.0.100-preview.7 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["global.json", "./"]
COPY ["ZootopiaAio.App/ZootopiaAio.App.csproj", "ZootopiaAio.App/"]
RUN dotnet restore "ZootopiaAio.App/ZootopiaAio.App.csproj"

COPY . .
RUN dotnet publish "ZootopiaAio.App/ZootopiaAio.App.csproj" \
        -c "$BUILD_CONFIGURATION" \
        --no-restore \
        -p:PublishBlazorGateway=true \
        -o /out/app

FROM mcr.microsoft.com/dotnet/aspnet:11.0.0-preview.7 AS final

COPY --from=build /out/app /app
WORKDIR /app

ENV ASPNETCORE_CONTENTROOT=/app \
    ClientApps__app__EndpointsManifest=/app/ZootopiaAio.App.staticwebassets.endpoints.json \
    Gateway__HttpsRedirection__Enabled=false \
    ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

EXPOSE 8080
USER $APP_UID

ENTRYPOINT ["dotnet", "/app/gateway/blazor-gateway.dll"]
