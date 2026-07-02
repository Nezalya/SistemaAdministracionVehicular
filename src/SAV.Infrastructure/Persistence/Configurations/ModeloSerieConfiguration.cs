namespace SAV.Infrastructure.Persistence.Configurations;

public class ModeloSerieConfiguration : IEntityTypeConfiguration<ModeloSerie>
{
    public void Configure(EntityTypeBuilder<ModeloSerie> builder)
    {
        builder.HasKey(ms => new { ms.ModeloId, ms.CaracteristicaId });

        builder.HasOne(ms => ms.Modelo)
            .WithMany(m => m.EquipamientoDeSerie)
            .HasForeignKey(ms => ms.ModeloId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ms => ms.Caracteristica)
            .WithMany(c => c.ModelosDeSerie)
            .HasForeignKey(ms => ms.CaracteristicaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}