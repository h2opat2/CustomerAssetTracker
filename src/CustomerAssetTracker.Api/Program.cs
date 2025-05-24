using Microsoft.EntityFrameworkCore;
using CustomerAssetTracker.Core.Data;
using CustomerAssetTracker.Core.Abstractions;
using CustomerAssetTracker.Core.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var dbPath = Path.Combine(builder.Environment.ContentRootPath, "..", "..", "CustomerAssetTracker.db");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

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
    