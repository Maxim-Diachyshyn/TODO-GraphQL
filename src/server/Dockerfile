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
COPY --from=build ["/app/Presentation/TODOGraphQL.Api.GraphQL/out", "./"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet TODOGraphQL.Api.GraphQL.dll
