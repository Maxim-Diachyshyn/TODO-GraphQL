FROM microsoft/dotnet:2.2.100-sdk AS build
WORKDIR /app
COPY ["./src/server/", "./"]
RUN dotnet restore

WORKDIR /app
RUN dotnet publish -c Release -o out


FROM node:latest AS web-build
WORKDIR /app
COPY ["./src/client/", "./"]

WORKDIR /app
RUN yarn
WORKDIR /app
RUN yarn build

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build ["/app/Presentation/TODOGraphQL.Api.GraphQL/out", "./"]
COPY --from=web-build ["/app/build", "./"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet TODOGraphQL.Api.GraphQL.dll
