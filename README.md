# POS API

A point-of-sale backend built with ASP.NET Core 6, Entity Framework Core, and SQL Server. The solution separates HTTP endpoints, application use cases, domain models, and persistence into four projects.

## Current scope

The API currently exposes endpoints for:

- Creating categories.
- Creating products with a price, SKU, stock quantity, categories, sizes, and side dishes.

The codebase also contains order commands and customer/member models, but these are not exposed through HTTP controllers. Listing, updating, and deleting products or categories are not implemented as API endpoints.

## Technology

| Component | Technology |
| --- | --- |
| Runtime and web framework | .NET 6 / ASP.NET Core |
| Persistence | Entity Framework Core 6.0.27 |
| Database | SQL Server |
| Application dispatch | MediatR 12.2.0 |
| API documentation | Swagger / Swashbuckle 6.5.0 |

## Getting started

### Prerequisites

- Git.
- .NET 6 SDK.
- A running SQL Server instance, such as SQL Server Express, and an account with permission to create or update the development database.
- Entity Framework CLI matching the repository's EF Core version.

Run these commands from the repository root unless noted otherwise.

### 1. Clone and build

```sh
git clone https://github.com/jmgabriel13/pos-api.git
cd pos-api
dotnet restore pos-backend.sln
dotnet build pos-backend.sln --no-restore
```

### 2. Configure the database

The application reads `ConnectionStrings:DefaultConnection`. The connection string in [appsettings.json](Pos-api/appsettings.json) points to the original developer's SQL Server instance, so override it for your machine.

For a local SQL Server Express instance with Windows authentication, set the following environment variable in PowerShell:

```powershell
$env:ConnectionStrings__DefaultConnection = 'Server=localhost\SQLEXPRESS;Database=pos_db;Integrated Security=True;TrustServerCertificate=True'
```

Replace `localhost\SQLEXPRESS` with your SQL Server instance. Keep this terminal open for the migration and run commands; this override applies to processes launched from it.

For SQL Server authentication, use a connection string in the form below as the environment variable's value, replacing the placeholders locally:

```text
Server=<server>,1433;Database=pos_db;User Id=<user>;Password=<password>;TrustServerCertificate=True
```

Keep credentials out of committed configuration files. The certificate setting in these examples is intended for local development.

### 3. Apply database migrations

If `dotnet-ef` is not installed:

```sh
dotnet tool install --global dotnet-ef --version 6.0.27
```

Check `dotnet ef --version` if you already have the tool installed; use version 6.0.27 for these instructions.

Apply the checked-in migrations:

```sh
dotnet ef database update --project Infrastructure/Infrastructure.csproj --startup-project Pos-api/Pos-api.csproj
```

Migrations and the database context live in `Infrastructure`; `Pos-api` supplies the startup configuration and dependency registration. The API does not apply migrations automatically at startup.

### 4. Run the API

On Windows or macOS, trust the local ASP.NET Core HTTPS development certificate if needed:

```sh
dotnet dev-certs https --trust
```

Start the application using its development launch profile:

```sh
dotnet run --project Pos-api/Pos-api.csproj --launch-profile Pos_api
```

The checked-in launch profile configures:

| Resource | URL |
| --- | --- |
| HTTPS API base URL | https://localhost:7241 |
| HTTP listener | http://localhost:5173 |
| Swagger UI | https://localhost:7241/swagger |
| OpenAPI document | https://localhost:7241/swagger/v1/swagger.json |

HTTP requests are redirected to HTTPS. Swagger is enabled only in the `Development` environment, which the `Pos_api` launch profile selects.

## API usage

Use Swagger's **Try it out** action to submit the example requests below. Both endpoints accept `Content-Type: application/json`.

| Method | Route | Purpose |
| --- | --- | --- |
| POST | `/api/category/create` | Create a category |
| POST | `/api/products/create` | Create a product |

### Create a category

`POST /api/category/create`

```json
{
  "name": "Main dishes"
}
```

### Create a product

`POST /api/products/create`

```json
{
  "name": "Chicken rice",
  "description": "Grilled chicken with rice",
  "price": 150.00,
  "sku": "PROD00000000001",
  "stocks": 50,
  "categories": ["Main dishes"],
  "sizes": ["Regular", "Large"],
  "sideDishes": ["Soup"]
}
```

Request details:

- `price` is a numeric amount; the handler assigns the currency `PHP`.
- A non-empty `sku` must contain exactly 15 characters. Empty values are accepted by the SKU factory; other lengths return null and are not handled as a structured validation failure by the create handler. Use a valid SKU.
- `categories`, `sizes`, and `sideDishes` are arrays of strings. Supply `[]` when there are no values.
- Product categories are stored as strings, not category IDs; the create handler does not look them up in the category table.
- Avoid `|` inside array values because the persistence mappings use it as a delimiter.

### Responses

Both create handlers return HTTP `200 OK` with a success result after saving:

```json
{
  "isSuccess": true,
  "isFailure": false,
  "error": {
    "code": "",
    "message": ""
  }
}
```

The response does not include the created entity or its ID. The shared controller maps application `Result` failures to HTTP `400` with `ProblemDetails`; the current create handlers return success after saving and do not convert database exceptions into that failure format.

## Project structure

```text
pos-api/
├── Domain/                 # Entities, value objects, domain events, repository contracts, results
├── Application/            # Commands, handlers, services, and persistence abstractions
├── Infrastructure/         # EF Core context, mappings, repositories, and migrations
├── Pos-api/                # Controllers, application startup, configuration, and Swagger
└── pos-backend.sln          # Solution containing all four projects
```

Project references follow `Pos-api → Infrastructure → Application → Domain`. Startup registers application handlers and infrastructure services. HTTP controllers dispatch commands through MediatR; handlers use repository and unit-of-work abstractions to persist changes.

## Development and verification

Build the full solution:

```sh
dotnet build pos-backend.sln
```

After changing the EF Core model, create a migration with a descriptive name:

```sh
dotnet ef migrations add DescribeYourChange --project Infrastructure/Infrastructure.csproj --startup-project Pos-api/Pos-api.csproj
```

Review the generated migration before applying it with the database update command above.

No automated test project or CI workflow is currently checked in. For a manual smoke check, apply migrations to a development database, start the API, submit both example requests in Swagger, and confirm that rows appear in the `Categories` and `Products` tables.

## Troubleshooting

| Symptom | Check |
| --- | --- |
| SQL Server connection or login failure | Confirm the instance is running, the connection string matches your authentication method, and the terminal still has the environment override set. |
| Missing database tables | Apply migrations using `Infrastructure` as the target project and `Pos-api` as the startup project. |
| `dotnet ef` is not found | Install the EF CLI and ensure the .NET global tools directory is on your PATH. |
| HTTPS certificate error | Trust the ASP.NET Core development certificate on supported platforms. |
| Swagger returns 404 | Run with the `Pos_api` launch profile and use the HTTPS URL above; Swagger is only enabled in Development. |

## License

No license file is currently included in this repository.
