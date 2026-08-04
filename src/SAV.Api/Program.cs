using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SAV.Application;
using SAV.Api.Extensions;
using SAV.Application.Mappings;
using SAV.Infrastructure;
using SAV.Infrastructure.Persistence;
using SAV.Application.Common;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();//Injeccion de dependencias

builder.Services.AddInfrastructure(builder.Configuration);//Injeccion de dependencias de infraestructura

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));//registro del mapeo 

builder.Services.AddControllers()//Para el registro de los converts
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));
builder.Services.Configure<MvcOptions>(options =>
{
    options.Filters.Add<EnvolturaRespuestaFilter>();
});

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddApiValidation();
builder.Services.AddApiDocumentation();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SavDbContext>();
    SAV.Infrastructure.Data.DataSeeder.SembrarAdministrador(context, builder.Configuration);
}

app.UseMiddleware<SAV.Api.Middleware.ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();