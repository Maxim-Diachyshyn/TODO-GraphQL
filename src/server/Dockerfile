FROM microsoft/dotnet:2.2.100-sdk AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ["./", "./"]
RUN dotnet restore

# copy everything else and build app
WORKDIR /app
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build ["/app/Presentation/FilmCatalogue.Api.GraphQL/out", "./"]
ENTRYPOINT ["dotnet", "FilmCatalogue.Api.GraphQL.dll"]
