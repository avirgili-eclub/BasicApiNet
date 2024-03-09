using BasicApiNet.Access;
using BasicApiNet.Access.Data;
using BasicApiNet.Access.Repository;
using BasicApiNet.Access.Services;
using BasicApiNet.Core.Models;
using BasicApiNet.Core.Repository;
using BasicApiNet.Core.Services;
using BasicApiNet.Middleware;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddConsole();
// Add logging
builder.Services.AddLogging();

builder.Services.AddControllers();

//Sql Dependency Injection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlite(connectionString));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basic API", Version = "v1" });
});

#region Service Injected
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ICityRepository), typeof(CityRepository));
builder.Services.AddScoped(typeof(ICountryRepository), typeof(CountryRepository));
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICommonService<Country>, CountryService>();
builder.Services.AddScoped<ICountryService, CountryService>();
#endregion

#region other services injected
builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
#endregion
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

// Configuración del middleware de validación de token de antifalsificación
app.Use(async (_, next) =>
{
    var mvcOptions = app.Services.GetRequiredService<MvcOptions>();
    mvcOptions.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    await next.Invoke();
});

// Seed database
await Seed.EnsureSeedData(app.Services);

app.Run();