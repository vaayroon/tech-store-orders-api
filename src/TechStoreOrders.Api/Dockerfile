FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/TechStoreOrders.Api/TechStoreOrders.Api.csproj", "TechStoreOrders.Api/"]
COPY ["src/TechStoreOrders.Application/TechStoreOrders.Application.csproj", "TechStoreOrders.Application/"]
COPY ["src/TechStoreOrders.Domain/TechStoreOrders.Domain.csproj", "TechStoreOrders.Domain/"]
COPY ["src/TechStoreOrders.Infrastructure/TechStoreOrders.Infrastructure.csproj", "TechStoreOrders.Infrastructure/"]
RUN dotnet restore "TechStoreOrders.Api/TechStoreOrders.Api.csproj"

COPY src/ .
WORKDIR "/src/TechStoreOrders.Api"
RUN dotnet publish "TechStoreOrders.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .
RUN mkdir -p /data

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "TechStoreOrders.Api.dll"]
