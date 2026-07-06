using Microsoft.EntityFrameworkCore;
using SAV.Api;
using SAV.Infrastructure.Persistence;
using Scalar.AspNetCore;//Scalar Reference

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<SavDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddServices();//Injeccion de dependencias


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();//Scalar Referenc
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();