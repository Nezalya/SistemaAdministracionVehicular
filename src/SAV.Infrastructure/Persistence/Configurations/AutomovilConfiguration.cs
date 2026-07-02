namespace SAV.Infrastructure.Persistence.Configurations;

public class AutomovilConfiguration : IEntityTypeConfiguration<Automovil>
{
    public void Configure(EntityTypeBuilder<Automovil> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.NumBastidor)
            .IsRequired()
            .HasMaxLength(17);

        builder.HasIndex(a => a.NumBastidor)
            .IsUnique();

        builder.Property(a => a.Color)
            .HasMaxLength(50);

        builder.Property(a => a.Estado)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.HasOne(a => a.Modelo)
            .WithMany(m => m.Automoviles)
            .HasForeignKey(a => a.ModeloId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.ConcesionarioPropietario)
            .WithMany(c => c.AutomovilesPropios)
            .HasForeignKey(a => a.ConcesionarioPropietarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.ConcesionarioUbicacion)
            .WithMany(c => c.AutomovilesUbicados)
            .HasForeignKey(a => a.ConcesionarioUbicacionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.ServicioOficialUbicacion)
            .WithMany(s => s.AutomovilesUbicados)
            .HasForeignKey(a => a.ServicioOficialUbicacionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}