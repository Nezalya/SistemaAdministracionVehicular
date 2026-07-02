namespace SAV.Infrastructure.Persistence.Configurations;

public class VendedorConfiguration : IEntityTypeConfiguration<Vendedor>
{
    public void Configure(EntityTypeBuilder<Vendedor> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Nombre)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(v => v.Nif)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(v => v.Nif)
            .IsUnique();

        builder.Property(v => v.Domicilio)
            .HasMaxLength(200);

        builder.Property(v => v.Telefono)
            .HasMaxLength(20);

        builder.Property(v => v.Email)
            .HasMaxLength(100);

        builder.HasOne(v => v.Concesionario)
            .WithMany(c => c.Vendedores)
            .HasForeignKey(v => v.ConcesionarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}