using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace SAV.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void SembrarAdministrador(SavDbContext context, IConfiguration config)
        {
            if (context.Usuarios.Any(u => u.Rol == RolUsuario.Administrador))
                return;

            var admin = new Usuario
            {
                Username = "admin",
                Email = config["AdminSeed:Email"] ?? "admin@sav.com",
                Rol = RolUsuario.Administrador,
                Activo = true,
                FechaRegistro = DateTime.Now
            };
            admin.PasswordHash = new PasswordHasher<Usuario>()
                .HashPassword(admin, config["AdminSeed:Password"] ?? "Admin123!");

            context.Usuarios.Add(admin);
            context.SaveChanges();
        }
    }
}