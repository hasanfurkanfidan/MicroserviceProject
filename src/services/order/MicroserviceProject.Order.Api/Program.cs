using MicroserviceProject.Order.Persistence;
using MicroserviceProject.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));
builder.Services.AddDbContext<AppDbContext>(option => { option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")); });

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();