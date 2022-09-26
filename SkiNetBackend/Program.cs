using Microsoft.EntityFrameworkCore;
using SkiNetBackend.Data;
using SkiNetBackend.Helpers;
using SkiNetBackend.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreContext>(
    x => x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));

// Adding automapper
builder.Services.AddAutoMapper(typeof(MappingProfiles));

var app = builder.Build();

// Use custom middleware
// app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Rum migrations on startup

    await using var scope = app.Services.CreateAsyncScope();
    using var db = scope.ServiceProvider.GetService<StoreContext>();
    await db.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// adding use static files
app.UseStaticFiles();

app.MapControllers();

app.Run();
