FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RestfulAPI/RestfulAPI.csproj", "RestfulAPI/"]
RUN dotnet restore "RestfulAPI/RestfulAPI.csproj"
COPY . .
WORKDIR "/src/RestfulAPI"
RUN dotnet build "RestfulAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RestfulAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestfulAPI.dll"]
