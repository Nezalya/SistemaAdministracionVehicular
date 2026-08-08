using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Radzen;
using SAV.UI.Components;
using SAV.UI.Services.Api;
using SAV.UI.Services.Auth;
using SAV.UI.Services.Ui;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ---------- Radzen ----------
// Servicios de diálogos, notificaciones, tooltips y menús contextuales.
// El JavaScript de Radzen se carga en App.razor: sin él estos servicios no operan.
builder.Services.AddRadzenComponents();
builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "SAV.Theme";
    options.Duration = TimeSpan.FromDays(365);
});

// ---------- Autenticación ----------
// La cookie transporta el JWT como claim: cifrada por Data Protection y HttpOnly,
// el token nunca queda accesible desde el navegador.
var duracionSesion = TimeSpan.FromMinutes(
    builder.Configuration.GetValue<int?>("Sesion:DuracionMinutos") ?? 120);

builder.Services.AddCascadingAuthenticationState();
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "SAV.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.AccessDeniedPath = "/acceso-denegado";
        options.ExpireTimeSpan = duracionSesion;
        options.SlidingExpiration = false; // El JWT no se renueva, así que la cookie tampoco.
    });
builder.Services.AddAuthorization();

// ---------- Servicios de la UI ----------
builder.Services.AddScoped<ITokenAccessor, TokenAccessor>();
builder.Services.AddScoped<SesionUsuario>();
builder.Services.AddScoped<AvisosUi>();
builder.Services.AddApiClients(builder.Configuration, builder.Environment);

var app = builder.Build();

// ---------- Cultura ----------
var culturas = new[] { new CultureInfo("es-ES") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(culturas[0]),
    SupportedCultures = culturas,
    SupportedUICultures = culturas
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
