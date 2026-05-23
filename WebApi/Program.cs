using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Persistence.DataContext;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApi.Middlewares;

Log.Logger = new LoggerConfiguration().WriteTo.Console().WriteTo.File("logs/log.txt").CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IStockAdjustmentService, StockAdjustmentService>();
builder.Services.AddScoped<IAnalyticService, AnalyticService>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionMiddleware1>();
app.UseMiddleware<ExceptionMiddleware2>();
app.MapControllers();
app.Run();