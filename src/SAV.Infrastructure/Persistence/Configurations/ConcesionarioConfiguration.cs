namespace SAV.Infrastructure.Persistence.Configurations;

public class ConcesionarioConfiguration : IEntityTypeConfiguration<Concesionario>
{
    public void Configure(EntityTypeBuilder<Concesionario> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nombre)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(c => c.Nif)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(c => c.Nif)
            .IsUnique();

        builder.Property(c => c.Domicilio)
            .HasMaxLength(200);

        builder.Property(c => c.Telefono)
            .HasMaxLength(20);

        builder.Property(c => c.Email)
            .HasMaxLength(100);
    }
}