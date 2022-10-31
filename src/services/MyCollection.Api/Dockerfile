FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/services/MyCollection.Api/MyCollection.Api.csproj", "src/services/MyCollection.Api/"]
COPY ["src/services/MyCollection.Data/MyCollection.Data.csproj", "src/services/MyCollection.Data/"]
COPY ["src/services/MyCollection.Domain/MyCollection.Domain.csproj", "src/services/MyCollection.Domain/"]
RUN dotnet restore "src/services/MyCollection.Api/MyCollection.Api.csproj"
COPY . .
WORKDIR "/src/src/services/MyCollection.Api"
RUN dotnet build "MyCollection.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyCollection.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyCollection.Api.dll"]