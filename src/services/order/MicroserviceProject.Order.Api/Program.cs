using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using MicroserviceProject.Order.Api.Endpoints;
using MicroserviceProject.Order.Application.Contracts.Repositories;
using MicroserviceProject.Order.Application.Contracts.UnitOfWorks;
using MicroserviceProject.Order.Persistence;
using MicroserviceProject.Order.Persistence.Repositories;
using MicroserviceProject.Order.Persistence.UnitOfWork;
using MicroserviceProject.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// ---- Core / Shared ----
builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(OrderApiAssembly)));

// ---- API Versioning (CRITICAL: fixes IReportApiVersions error) ----
builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;

        // Bu true olmazsa bazý senaryolarda IReportApiVersions yokmuþ gibi patlayabiliyor
        options.ReportApiVersions = true;
    })
    .AddApiExplorer(options =>
    {
        // Swagger group name: v1 / v1.0 / v2 ...
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

// ---- OpenAPI / Swagger ----
builder.Services.AddEndpointsApiExplorer();

// Swagger dokümanlarýný versiyonlara göre üretelim
builder.Services.AddSwaggerGen(options =>
{
    // Versiyonlarý swagger’a baðlamak için DI’dan provider alacaðýz
    // (Bu provider AddApiExplorer ile gelir)
    using var sp = builder.Services.BuildServiceProvider();
    var provider = sp.GetRequiredService<IApiVersionDescriptionProvider>();

    foreach (var desc in provider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(desc.GroupName, new OpenApiInfo
        {
            Title = "MicroserviceProject.Order.Api",
            Version = desc.ApiVersion.ToString(),
            Description = desc.IsDeprecated ? "This API version has been deprecated." : null
        });
    }

    // Eðer endpointlerde XML comment kullanýyorsan burayý aç:
    // var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

// Ýstersen yeni .NET OpenAPI (AddOpenApi / MapOpenApi) kullanýyorsun ama
// SwaggerGen zaten iþini görüyor. Senin mevcut akýþ bozulmasýn diye býrakýyorum:
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // .NET built-in OpenAPI endpoint (isteðe baðlý)
    app.MapOpenApi();

    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        // Burada da versiyonlarýn hepsini UI’a ekliyoruz
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var desc in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
        }

        // opsiyonel güzelleþtirme
        options.DisplayRequestDuration();
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });
}

app.UseHttpsRedirection();

// ---- Your endpoint mapping ----
// Senin extensionlarýn version set vs kullanýyor.
// Artýk IReportApiVersions DI’da var, patlamaz.
app.AddOrderGroupEndpointExt(app.AddVersionSetExt());

app.UseAuthentication();
app.UseAuthorization();
app.Run();
