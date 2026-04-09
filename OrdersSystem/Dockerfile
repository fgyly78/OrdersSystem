FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OrdersSystem/OrdersSystem.csproj", "OrdersSystem/"]
COPY ["OrdersSystem.Application/OrdersSystem.Application.csproj", "OrdersSystem.Application/"]
COPY ["OrdersSystem.Domain/OrdersSystem.Domain.csproj", "OrdersSystem.Domain/"]
COPY ["OrdersSystem.Infrastructure/OrdersSystem.Infrastructure.csproj", "OrdersSystem.Infrastructure/"]
RUN dotnet restore "OrdersSystem/OrdersSystem.csproj"
COPY . .
WORKDIR "/src/OrdersSystem"
RUN dotnet build "./OrdersSystem.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrdersSystem.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrdersSystem.dll"]
