namespace SAV.Infrastructure.Persistence.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.Rol)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.HasOne(u => u.Vendedor)
            .WithOne(v => v.Usuario)
            .HasForeignKey<Usuario>(u => u.VendedorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(u => u.Concesionario)
            .WithMany(c => c.Usuarios)
            .HasForeignKey(u => u.ConcesionarioId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}