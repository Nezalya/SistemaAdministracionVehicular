namespace SAV.Infrastructure.Persistence.Configurations;

public class MarcaConfiguration : IEntityTypeConfiguration<Marca>
{
    public void Configure(EntityTypeBuilder<Marca> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.PaisOrigen)
            .HasMaxLength(100);

        builder.HasIndex(m => m.Nombre)
            .IsUnique();
    }
}