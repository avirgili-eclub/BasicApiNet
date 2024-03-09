using System.Text;
using BasicApiNet.Access;
using BasicApiNet.Access.Data;
using BasicApiNet.Access.Repository;
using BasicApiNet.Access.Services;
using BasicApiNet.Core.Models;
using BasicApiNet.Core.Repository;
using BasicApiNet.Core.Services;
using BasicApiNet.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
builder.Services.AddSwaggerGen(o=>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "Basic API", Version = "v1", Description = "Basic API"});
    
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            //TODO: Implement later roles or scopes required to access endpoints
            new string[]{}
        }
    });
    
});

//TODO: implement the secret key from AWS Secrets Manager or Azure Key Vault 

var secretKey = builder.Configuration["JwtSettings:SecretKey"];

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "UMBRELLA",
            ValidAudience = "http://localhost:5228", // for future reference: should match the 'audience' parameter in JwtSecurityToken
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
        };
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

app.UseAuthentication();

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