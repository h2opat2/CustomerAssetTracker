# Customer Asset Tracker

This project presents a web application for managing customers, their purchased machines, licenses, and service records. It is designed with an emphasis on modern .NET technologies, object-oriented programming (OOP) principles, design patterns, and efficient data handling.

## Table of Contents

* [Project Overview](#project-overview)
* [Architecture and Key Components](#architecture-and-key-components)
    * [Domain Entities](#domain-entities)
    * [Database Layer (Entity Framework Core)](#database-layer-entity-framework-core)
    * [Design Patterns](#design-patterns)
    * [Web API (Backend)](#web-api-backend)
* [Project Structure](#project-structure)
* [How to Run the Project](#how-to-run-the-project)
* [Testing](#testing)
* [Future Plans](#future-plans)
* [Contributing](#contributing)

---

## Project Overview

The goal of the project is to create a robust system for recording and managing customer assets, which includes:
* **Customers:** Basic information about customers.
* **Machines:** Details about purchased machines, including specific types (CMM, Arm).
* **Licenses:** Information about software licenses tied to machines or customers.
* **Service Records:** History of service interventions for each machine.

The project is divided into phases and serves as a practical demonstration of .NET development.

---

## Architecture and Key Components

The project is built on a layered architecture with a clear separation of concerns.

### Domain Entities

Defined in the `CustomerAssetTracker.Core` project, these represent the fundamental business objects:
* `Customer`
* `Machine` (base class for machines)
* `Cmm` and `Arm` (inheriting from `Machine` with their own properties and validation)
* `License`
* `ServiceRecord`

All entities include an `Id` for unique identification and navigation properties to define relationships (e.g., `Customer` has a `List<Machine> Machines`).

### Database Layer (Entity Framework Core)

Ensures data persistence.
* **SQLite:** A lightweight, file-based database (`CustomerAssetTracker.db`) for easy development and testing.
* **Entity Framework Core (EF Core):** An ORM for working with the database using C# objects.
    * **`ApplicationDbContext`:** The main context for database interaction, mapping entities to tables, and containing **data seeding** logic.
    * **Migrations:** Managed using `dotnet ef`, allowing tracking changes in the database schema and applying them.
    * **`ApplicationDbContextFactory`:** Ensures proper `DbContext` initialization for EF Core tools at design time.

### Design Patterns

The following patterns are used to improve code structure, maintainability, and testability:
* **Repository Pattern:**
    * **`IGenericRepository<T>`:** An interface defining common CRUD operations.
    * **`GenericRepository<T>`:** A concrete implementation of `IGenericRepository<T>` utilizing EF Core. Extended with support for **eager loading** (`.Include()`) to load related data.
* **Unit of Work Pattern:**
    * **`IUnitOfWork`:** An interface for managing transactions and coordinating repositories.
    * **`UnitOfWork`:** An implementation of `IUnitOfWork` that ensures atomic saving of all changes within a single business transaction.

### Web API (Backend)

Provides RESTful HTTP services for data communication.
* **ASP.NET Core Web API:** Framework for building APIs.
* **`Program.cs`:** Configures Dependency Injection (DI) for `DbContext`, `UnitOfWork`, and `IMapper`. Also includes Swagger/OpenAPI configuration.
* **API Controllers:**
    * `CustomersController`, `MachinesController`, `LicensesController`, `ServiceRecordsController`.
    * Handle HTTP requests (GET, POST, PUT, DELETE, PATCH) and use `IUnitOfWork` to interact with the data layer.
* **Data Transfer Objects (DTOs):**
    * Simple classes for transferring data between the API and the client (`[Entity]Dto`, `Create[Entity]Dto`, `Update[Entity]Dto`, `Patch[Entity]Dto`).
    * Include validation attributes (`[Required]`, `[MaxLength]`).
* **AutoMapper:**
    * **`IMapper`:** An injected instance used for automatic mapping between domain entities and DTOs, reducing boilerplate code.
    * **Mapping Profiles:** Classes inheriting from `AutoMapper.Profile` where mapping rules are defined.

## Project Structure
```bash
CustomerAssetTracker/
├── CustomerAssetTracker.sln
├── src/
│   ├── CustomerAssetTracker.Core/         # Domain entities, DbContext, interfaces, repositories
│   │   ├── Data/                          # ApplicationDbContext, ApplicationDbContextFactory
│   │   │   └── Repositories/              # GenericRepository, UnitOfWork
│   │   ├── Abstractions/                  # IGenericRepository, IUnitOfWork
│   │   ├── Entities/                      # Customer, Machine, License, ServiceRecord, Cmm, Arm
│   │   └── CustomerAssetTracker.Core.csproj
│   └── CustomerAssetTracker.Api/          # ASP.NET Core Web API project
│       ├── Controllers/                   # API Controllers (CustomersController, etc.)
│       ├── DTOs/                          # Data Transfer Objects (CustomerDto, MachineDto, etc.)
│       ├── MappingProfiles/               # AutoMapper profiles
│       ├── Properties/                    # launchSettings.json
│       ├── Program.cs                     # DI, Swagger, middleware configuration
│       └── CustomerAssetTracker.Api.csproj
└── test/
└── CustomerAssetTracker.Core.Tests/   # Unit tests for CustomerAssetTracker.Core
└── CustomerAssetTracker.Core.Tests.csproj
```
## How to Run the Project

To run the project, you need the .NET SDK (version 9.0 or newer).

1.  **Clone the repository:**
    ```bash
    git clone <URL_REPOSITARE>
    cd CustomerAssetTracker
    ```

2.  **Restore NuGet packages and build the solution:**
    Open a terminal in the solution's root folder (`CustomerAssetTracker/`) and run:
    ```bash
    dotnet restore
    dotnet build
    ```

3.  **Apply database migrations and seed data:**
    Navigate to the `CustomerAssetTracker.Core` folder:
    ```bash
    cd src/CustomerAssetTracker.Core
    dotnet ef database update --project .\CustomerAssetTracker.Core.csproj
    ```
    This will create the `CustomerAssetTracker.db` database file and populate it with sample data.

4.  **Run the Web API:**
    Navigate to the `CustomerAssetTracker.Api` folder:
    ```bash
    cd ../CustomerAssetTracker.Api
    dotnet watch run
    ```
    The application will start, and a web browser should automatically open to the Swagger UI address (e.g., `https://localhost:7113/swagger`). If the browser does not open, check the terminal output for the URL and open it manually.

## Testing

The project includes unit tests for the domain logic.
* **Run unit tests:**
    Navigate to the solution's root folder (`CustomerAssetTracker/`) and run:
    ```bash
    dotnet test
    ```

## Future Plans

* Implement a Blazor frontend application to interact with the API.
* Add authentication and authorization.
* Advanced filtering, searching, and pagination.
* Improved error handling.

## Contributing

We welcome any contributions! If you find a bug or have an idea for an improvement, feel free to open an "issue" or a "pull request".
