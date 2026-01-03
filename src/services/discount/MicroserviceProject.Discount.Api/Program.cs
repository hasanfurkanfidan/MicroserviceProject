using MicroserviceProject.Discount.Api;
using MicroserviceProject.Discount.Api.Features.Discounts;
using MicroserviceProject.Discount.Api.Options;
using MicroserviceProject.Discount.Api.Repositories;
using MicroserviceProject.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddVersionExt();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddCommonServiceExt(typeof(DiscountAssembly));

var app = builder.Build();
app.AddDiscountGroupEndpointExt(app.AddVersionSetExt());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();


app.Run();