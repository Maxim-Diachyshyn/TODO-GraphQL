FROM microsoft/dotnet:2.2.100-sdk AS build
WORKDIR /app
COPY ["./src/server/*.sln", "./"]
COPY ["./src/server/Core/Application/TODOGraphQL.Application/TODOGraphQL.Application.csproj", "./Core/Application/TODOGraphQL.Application/TODOGraphQL.Application.csproj"]
COPY ["./src/server/Core/Domain/TODOGraphQL.Domain/TODOGraphQL.Domain.csproj", "./Core/Domain/TODOGraphQL.Domain/TODOGraphQL.Domain.csproj"]
COPY ["./src/server/Infrastructure/TODOGraphQL.Infrastructure/TODOGraphQL.Infrastructure.csproj", "./Infrastructure/TODOGraphQL.Infrastructure/TODOGraphQL.Infrastructure.csproj"]
COPY ["./src/server/Persistence/TODOGraphQL.Persistence/TODOGraphQL.Persistence.csproj", "./Persistence/TODOGraphQL.Persistence/TODOGraphQL.Persistence.csproj"]
COPY ["./src/server/Presentation/TODOGraphQL.Api.GraphQL/TODOGraphQL.Api.GraphQL.csproj", "./Presentation/TODOGraphQL.Api.GraphQL/TODOGraphQL.Api.GraphQL.csproj"]
RUN dotnet restore

COPY ["./src/server/", "./"]
RUN dotnet publish -c Release -o out


FROM node:latest AS web-build
WORKDIR /app
COPY ["./src/client/package.json", "./"]
RUN yarn

COPY ["./src/client/", "./"]
RUN yarn build

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build ["/app/Presentation/TODOGraphQL.Api.GraphQL/out", "./"]
COPY --from=web-build ["/app/build", "./wwwroot"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet TODOGraphQL.Api.GraphQL.dll
