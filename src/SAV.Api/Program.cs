using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SAV.Api;
using SAV.Api.Mappings;
using SAV.Infrastructure;
using SAV.Infrastructure.Persistence;
using Scalar.AspNetCore;//Scalar Reference

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SavDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddServices();//Injeccion de dependencias

builder.Services.AddInfrastructure(builder.Configuration);//Injeccion de dependencias de infraestructura

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));//registro del mapeo 

builder.Services.AddControllers()//Para el registro de los converts
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));
builder.Services.Configure<MvcOptions>(options =>
{
    options.Filters.Add<SAV.Api.Responses.EnvolturaRespuestaFilter>();
});



var app = builder.Build();
app.UseMiddleware<SAV.Api.Middleware.ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();//Scalar Referenc
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();