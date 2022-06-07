using WebApiFHT;
using WebApiFHT.Entities;
using WebApiFHT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NLog.Web;
using WebApiFHT.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseNLog();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FHTDbContext>();
builder.Services.AddScoped<FHTSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IWebApiFHTService, FHTService>();
builder.Services.AddScoped< ErrorHandlingMiddleware > ();
builder.Services.AddScoped< RequestTimeMiddleware > ();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAccountService, AccountService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FHT Api");
    });
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();
app.UseHttpsRedirection();
var scope = app.Services.CreateScope();

var seeder = scope.ServiceProvider.GetService<FHTSeeder>();
seeder.Seed();
app.UseAuthorization();

app.MapControllers();

app.Run();
