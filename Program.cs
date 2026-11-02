using Dapper;
using DapperMediatRCrud.Data;
using DapperMediatRCrud.Features.Products.Commands;
using Npgsql;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string missing.");

builder.Services.AddScoped<NpgsqlConnection>(
    _ => new NpgsqlConnection(connectionString));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddMediatR(cfg =>  
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

var app = builder.Build();

// Database initialization
await using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<NpgsqlConnection>();

    await db.OpenAsync();

    await db.ExecuteAsync("""
        CREATE TABLE IF NOT EXISTS products (
            id BIGSERIAL PRIMARY KEY,
            name VARCHAR(150) NOT NULL,
            description TEXT,
            price NUMERIC(12,2) NOT NULL CHECK (price >= 0),
            created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
        );
    """);
    await db.ExecuteAsync("""
    CREATE TABLE IF NOT EXISTS categories (
        id BIGSERIAL PRIMARY KEY,
        category_name VARCHAR(100) NOT NULL UNIQUE,
        description TEXT,
        created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
    );
""");

    app.Logger.LogInformation("Database initialized successfully.");
}

// Swagger configuration
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dapper CRUD API V1");
});

app.MapControllers();

app.Run();