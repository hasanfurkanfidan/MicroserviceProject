using MicroserviceProject.Catalog.Api;
using MicroserviceProject.Catalog.Api.Features.Categories;
using MicroserviceProject.Catalog.Api.Features.Courses;
using MicroserviceProject.Catalog.Api.Options;
using MicroserviceProject.Catalog.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddCommonServiceExt(typeof(CatalogAssembly));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddCategoryGroupEndpointExt();
app.AddCourseGroupEndpointExt();

app.Run();
