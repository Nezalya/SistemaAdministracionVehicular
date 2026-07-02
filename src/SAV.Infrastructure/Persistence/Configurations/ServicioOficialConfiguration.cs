namespace SAV.Infrastructure.Persistence.Configurations;

public class ServicioOficialConfiguration : IEntityTypeConfiguration<ServicioOficial>
{
    public void Configure(EntityTypeBuilder<ServicioOficial> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Nombre)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(s => s.Nif)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(s => s.Nif)
            .IsUnique();

        builder.Property(s => s.Domicilio)
            .HasMaxLength(200);

        builder.Property(s => s.Telefono)
            .HasMaxLength(20);

        builder.HasOne(s => s.Concesionario)
            .WithMany(c => c.ServiciosOficiales)
            .HasForeignKey(s => s.ConcesionarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}