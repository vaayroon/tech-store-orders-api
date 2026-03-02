# Tech Store Orders API

## Overview
Tech Store Orders API is a layered ASP.NET Core application built with a Clean Architecture style split into:
- `TechStoreOrders.Api`: HTTP endpoints, middleware, and Swagger.
- `TechStoreOrders.Application`: use cases, commands, queries, and orchestration.
- `TechStoreOrders.Domain`: business rules, entities, and domain exceptions.
- `TechStoreOrders.Infrastructure`: EF Core SQLite persistence, repositories, migrations, and external integrations.
- `TechStoreOrders.Domain.Tests`: xUnit unit tests for domain business rules.

This design enforces SOLID principles:
- Single Responsibility: each layer has a focused purpose.
- Open/Closed: new handlers and integrations can be added without changing domain core.
- Liskov Substitution and Interface Segregation: repositories and services are accessed through narrow interfaces.
- Dependency Inversion: API/Application depend on abstractions, Infrastructure provides implementations.

## Database Setup (EF Core)
Run from repository root:

```bash
dotnet tool restore
dotnet tool run dotnet-ef database update \
  --project src/TechStoreOrders.Infrastructure/TechStoreOrders.Infrastructure.csproj \
  --startup-project src/TechStoreOrders.Api/TechStoreOrders.Api.csproj
```

## Run API
```bash
dotnet run --project src/TechStoreOrders.Api/TechStoreOrders.Api.csproj
```

Local URLs from `launchSettings.json`:
- HTTP: `http://localhost:5082`
- HTTPS: `https://localhost:7139` (also binds `http://localhost:5082`)
- Swagger UI: `http://localhost:5082/swagger` or `https://localhost:7139/swagger`

## Run With Docker Compose
```bash
docker compose up --build
```

Docker URL:
- `http://localhost:8080`
- Swagger UI: `http://localhost:8080/swagger`

## Run Tests
```bash
dotnet test TechStoreOrders.slnx
```

## How To Verify The 200 EUR Discount
Set `BASE_URL` to a running API URL:
- Local HTTP: `http://localhost:5082`
- Docker: `http://localhost:8080`

```bash
BASE_URL="http://localhost:5082"
```

1. Create an empty order:
```bash
curl -X POST "$BASE_URL/orders"
```
2. Add products so subtotal is greater than 200 EUR, for example two items at 120 EUR:
```bash
curl -X POST "$BASE_URL/orders/{orderId}/items" \
  -H "Content-Type: application/json" \
  -d '{"productId":"9DDBF7B4-6D18-4AB5-B5C5-43A4A4BE84BB","quantity":2}'
```
3. Query the order:
```bash
curl "$BASE_URL/orders/{orderId}?currency=EUR"
```
4. Expected result:
- Subtotal is `240.00` EUR.
- `DiscountAppliedEur` is `24.00` EUR (10%).
- `TotalEur` is `216.00` EUR.

## Notes
- Confirmed orders are immutable for item changes.
- Money is modeled with `decimal` end-to-end.
- Unsupported Frankfurter currencies fallback explicitly to `EUR` in output.
