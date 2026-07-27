# Order Management System

Order Management System is a full-stack academic project for managing bakery products, categories, customers, shopping carts, payment methods, and orders. The solution is organized around a clean, layered architecture so the domain model, application services, infrastructure, and API responsibilities remain separated and easier to maintain.

## Technology Stack

- **Frontend:** Angular 22
- **Backend:** ASP.NET Core / .NET 10
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core with Npgsql
- **Authentication:** JWT Bearer authentication
- **API Documentation:** Swagger / OpenAPI
- **Testing:** xUnit for backend service tests
- **Documentation:** UML diagrams and database scripts included in the repository

## Architecture Overview

The backend follows a Clean Architecture-inspired structure:

- **Domain:** entities, enums, exceptions, and repository contracts.
- **Application:** service interfaces and business use cases.
- **Infrastructure:** database context and repository implementations.
- **API:** controllers, middlewares, dependency injection, authentication, and HTTP endpoints.
- **Tests:** basic service tests focused on core business behavior.

The frontend is an Angular 22 application that consumes the backend API and provides the user interface for browsing products, categories, cart operations, and order management workflows.

## Main Features

- Product management by business and category
- Product search by name, description, or brand
- Category listing and parent-category filtering
- Order creation and order listing
- Shopping cart creation and status management
- Payment method listing
- JWT-based request context with header fallback
- UML and database documentation for system design support

## Repository Structure

```txt
database/                    PostgreSQL scripts and database assets
docs/                        UML diagrams and project documentation
order-management-asp-net/    ASP.NET Core .NET 10 backend
order-management-web/        Angular 22 frontend
```

## Backend Project Structure

```txt
OrderManagement.API/             HTTP API, controllers, middleware, extensions
OrderManagement.Application/     application services and use cases
OrderManagement.Domain/          domain entities and contracts
OrderManagement.Infrastructure/  EF Core context and repositories
OrderManagement.Tests/           xUnit backend tests
```

## Getting Started

### Backend

```bash
cd order-management-asp-net
dotnet restore
dotnet build
dotnet run --project OrderManagement.API
```

### Frontend

```bash
cd order-management-web
npm install
npm start
```

### Tests

```bash
cd order-management-asp-net
dotnet test OrderManagement.Tests/OrderManagement.Tests.csproj
```

## Documentation

- Database scripts are available in `database/`.
- UML diagrams are available in `docs/uml/`.
- The class diagram reflects the main domain model and its evolution during refactoring.
