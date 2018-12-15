FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 55267
EXPOSE 44369

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY FilmCatalogue/FilmCatalogue.csproj FilmCatalogue/
RUN dotnet restore FilmCatalogue/FilmCatalogue.csproj
COPY . .
WORKDIR /src/FilmCatalogue
RUN dotnet build FilmCatalogue.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish FilmCatalogue.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FilmCatalogue.dll"]
