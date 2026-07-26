using Microsoft.EntityFrameworkCore;
using OrderManagement.API.Extensions;
using OrderManagement.API.Middlewares;
using OrderManagement.Infrastructure.Connections;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add HttpClient
builder.Services.AddHttpClient();

// Add the PostgreSQL context.
builder.Services.AddDbContext<PostgreSQLContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection"));
});

// Declare all repositories and services
builder.Services.AddAppServices();
builder.Services.AddAppRespositories();

// Enable Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", policyBuilder =>
    {
        // Read cors from json file configuration.
        List<string> cors = [];
        builder.Configuration.GetSection("Cors").Bind(cors);
        cors.ForEach(cor => policyBuilder.WithOrigins(cor).Build());
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.SetIsOriginAllowed(_ => true);
    });
});

// Enable JWT.
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwaggerDocumentation();

app.UseCors("EnableCORS");

//app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.ConfigureExceptionMiddleware();

app.UseMiddleware<TokenValidationMiddleware>();

app.MapControllers();

app.Run();
