# Customer Asset Tracker

Tento projekt představuje webovou aplikaci pro správu zákazníků, jejich zakoupených strojů, licencí a servisních záznamů. Je navržen s důrazem na moderní .NET technologie, principy objektově orientovaného programování (OOP), návrhové vzory a efektivní práci s daty.

## Obsah

* [Přehled projektu](#přehled-projektu)
* [Architektura a klíčové komponenty](#architektura-a-klíčové-komponenty)
    * [Doménové Entity](#doménové-entity)
    * [Databázová Vrstva (Entity Framework Core)](#databázová-vrstva-entity-framework-core)
    * [Návrhové Vzory](#návrhové-vzory)
    * [Web API (Backend)](#web-api-backend)
* [Struktura projektu](#struktura-projektu)
* [Jak spustit projekt](#jak-spustit-projekt)
* [Testování](#testování)
* [Budoucí plány](#budoucí-plány)
* [Přispívání](#přispívání)

---

## Přehled projektu

Cílem projektu je vytvořit robustní systém pro evidenci a správu majetku zákazníků, který zahrnuje:
* **Zákazníky:** Základní informace o zákaznících.
* **Stroje:** Detaily o zakoupených strojích, včetně specifických typů (CMM, Arm).
* **Licence:** Informace o softwarových licencích vázaných na stroje nebo zákazníky.
* **Servisní Záznamy:** Historie servisních zásahů pro každý stroj.

Projekt je rozdělen do fází a slouží jako praktická ukázka vývoje v .NET.

---

## Architektura a klíčové komponenty

Projekt je postaven na vrstvené architektuře s jasným oddělením zodpovědností.

### Doménové Entity

Definované v projektu `CustomerAssetTracker.Core`, reprezentují základní obchodní objekty:
* `Customer`
* `Machine` (základní třída pro stroje)
* `Cmm` a `Arm` (dědící z `Machine` s vlastními vlastnostmi a validací)
* `License`
* `ServiceRecord`

Všechny entity obsahují `Id` pro unikátní identifikaci a navigační vlastnosti pro definování vztahů (např. `Customer` má `List<Machine> Machines`).

### Databázová Vrstva (Entity Framework Core)

Zajišťuje persistenci dat.
* **SQLite:** Lehká, souborová databáze (`CustomerAssetTracker.db`) pro snadný vývoj a testování.
* **Entity Framework Core (EF Core):** ORM pro práci s databází pomocí C# objektů.
    * **`ApplicationDbContext`:** Hlavní kontext pro interakci s databází, mapuje entity na tabulky a obsahuje logiku pro **seedování dat**.
    * **Migrace:** Spravovány pomocí `dotnet ef`, umožňují sledovat změny v databázovém schématu a aplikovat je.
    * **`ApplicationDbContextFactory`:** Zajišťuje správnou inicializaci `DbContextu` pro nástroje EF Core v době návrhu.

### Návrhové Vzory

Pro zlepšení struktury, udržitelnosti a testovatelnosti kódu jsou použity následující vzory:
* **Repository Pattern:**
    * **`IGenericRepository<T>`:** Rozhraní definující obecné CRUD operace.
    * **`GenericRepository<T>`:** Konkrétní implementace `IGenericRepository<T>` využívající EF Core. Rozšířen o podporu **eager loadingu** (`.Include()`) pro načítání souvisejících dat.
* **Unit of Work Pattern:**
    * **`IUnitOfWork`:** Rozhraní pro správu transakcí a koordinaci repozitářů.
    * **`UnitOfWork`:** Implementace `IUnitOfWork`, která zajišťuje atomické uložení všech změn v rámci jedné obchodní transakce.

### Web API (Backend)

Poskytuje RESTful HTTP služby pro komunikaci s daty.
* **ASP.NET Core Web API:** Framework pro tvorbu API.
* **`Program.cs`:** Konfigurace Dependency Injection (DI) pro `DbContext`, `UnitOfWork` a `IMapper`. Zahrnuje také konfiguraci Swagger/OpenAPI.
* **API Kontrolery:**
    * `CustomersController`, `MachinesController`, `LicensesController`, `ServiceRecordsController`.
    * Zpracovávají HTTP požadavky (GET, POST, PUT, DELETE, PATCH) a využívají `IUnitOfWork` pro interakci s datovou vrstvou.
* **Data Transfer Objects (DTOs):**
    * Jednoduché třídy pro přenos dat mezi API a klientem (`[Entity]Dto`, `Create[Entity]Dto`, `Update[Entity]Dto`, `Patch[Entity]Dto`).
    * Obsahují validační atributy (`[Required]`, `[MaxLength]`).
* **AutoMapper:**
    * **`IMapper`:** Používá se pro automatické mapování mezi doménovými entitami a DTOs, snižuje boilerplate kód.
    * **Mapping Profiles:** Třídy dědící z `AutoMapper.Profile`, kde jsou definována pravidla mapování.

## Struktura projektu

CustomerAssetTracker/
├── CustomerAssetTracker.sln
├── src/
│   ├── CustomerAssetTracker.Core/         # Doménové entity, DbContext, rozhraní, repozitáře
│   │   ├── Data/                          # ApplicationDbContext, ApplicationDbContextFactory
│   │   │   └── Repositories/              # GenericRepository, UnitOfWork
│   │   ├── Abstractions/                  # IGenericRepository, IUnitOfWork
│   │   ├── Entities/                      # Customer, Machine, License, ServiceRecord, Cmm, Arm
│   │   └── CustomerAssetTracker.Core.csproj
│   └── CustomerAssetTracker.Api/          # ASP.NET Core Web API projekt
│       ├── Controllers/                   # API Kontrolery (CustomersController atd.)
│       ├── DTOs/                          # Data Transfer Objects (CustomerDto, MachineDto atd.)
│       ├── MappingProfiles/               # AutoMapper profily
│       ├── Properties/                    # launchSettings.json
│       ├── Program.cs                     # Konfigurace DI, Swagger, middleware
│       └── CustomerAssetTracker.Api.csproj
└── test/
└── CustomerAssetTracker.Core.Tests/   # Unit testy pro CustomerAssetTracker.Core
└── CustomerAssetTracker.Core.Tests.csproj

## Jak spustit projekt

Pro spuštění projektu potřebuješ .NET SDK (verze 9.0 nebo novější).

1.  **Klonuj repozitář:**
    ```bash
    git clone <URL_REPOSITARE>
    cd CustomerAssetTracker
    ```

2.  **Obnov NuGet balíčky a sestav řešení:**
    Otevři terminál v kořenové složce řešení (`CustomerAssetTracker/`) a spusť:
    ```bash
    dotnet restore
    dotnet build
    ```

3.  **Aplikuj databázové migrace a seeduj data:**
    Naviguj se do složky `CustomerAssetTracker.Core`:
    ```bash
    cd src/CustomerAssetTracker.Core
    dotnet ef database update --project .\CustomerAssetTracker.Core.csproj
    ```
    Tím se vytvoří soubor databáze `CustomerAssetTracker.db` a naplní se ukázkovými daty.

4.  **Spusť Web API:**
    Naviguj se do složky `CustomerAssetTracker.Api`:
    ```bash
    cd ../CustomerAssetTracker.Api
    dotnet watch run
    ```
    Aplikace se spustí a automaticky by se měl otevřít webový prohlížeč na adrese Swagger UI (např. `https://localhost:7113/swagger`). Pokud se prohlížeč neotevře, zkontroluj výstup v terminálu pro URL a otevři ho ručně.

## Testování

Projekt obsahuje unit testy pro doménovou logiku.
* **Spuštění unit testů:**
    Naviguj se do kořenové složky řešení (`CustomerAssetTracker/`) a spusť:
    ```bash
    dotnet test
    ```

## Budoucí plány

* Implementace Blazor frontendové aplikace pro interakci s API.
* Přidání autentizace a autorizace.
* Pokročilé filtrování, vyhledávání a paginace.
* Vylepšení správy chyb.

## Přispívání

Vítáme jakékoli příspěvky! Pokud najdeš chybu nebo máš nápad na vylepšení, neváhej otevřít "issue" nebo "pull request".