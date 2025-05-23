    using Microsoft.EntityFrameworkCore;
    using CustomerAssetTracker.Core.Data; // Pro ApplicationDbContext
    using CustomerAssetTracker.Core.Abstractions; // Pro IUnitOfWork
    using CustomerAssetTracker.Core.Repositories; // Pro UnitOfWork
    using Microsoft.OpenApi.Models; // For OpenAPI/Swagger types
    using Swashbuckle.AspNetCore.SwaggerGen; // For AddSwaggerGen extension method
    using AutoMapper; // Důležité pro AutoMapper
    using CustomerAssetTracker.Api.MappingProfiles; // Pro tvůj mapovací profil


var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    // Komentář: Konfigurace DbContextu pro SQLite.
    // Používáme připojovací řetězec, který ukazuje na náš soubor databáze.
    // Důležité: Cesta k databázi je relativní k místu, odkud se aplikace spouští.
    // Pro ASP.NET Core Web API je to obvykle kořenová složka projektu.
    // Pokud je databáze v kořenové složce řešení, musíme se k ní dostat.
    var dbPath = Path.Combine(builder.Environment.ContentRootPath, "..", "..", "CustomerAssetTracker.db");
    // Alternativně, pokud je databáze přímo ve složce API projektu:
    // var dbPath = Path.Combine(builder.Environment.ContentRootPath, "CustomerAssetTracker.db");

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite($"Data Source={dbPath}"));

    // Komentář: Registrace Unit of Work a Generic Repository pro Dependency Injection.
    // AddScoped znamená, že pro každý HTTP požadavek bude vytvořena nová instance.
    // To je typické pro DbContext a Unit of Work.
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

    // Komentář: Registrace AutoMapperu.
    // Tímto se AutoMapper automaticky naskenuje a najde VŠECHNY třídy dědící z Profile
    // v aktuální assembly (tj. v API projektu).
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
    