namespace SAV.Infrastructure.Persistence.Configurations;

public class ModeloExtraConfiguration : IEntityTypeConfiguration<ModeloExtra>
{
    public void Configure(EntityTypeBuilder<ModeloExtra> builder)
    {
        builder.HasKey(me => new { me.ModeloId, me.CaracteristicaId });

        builder.Property(me => me.PrecioExtra)
            .HasColumnType("decimal(12,2)");

        builder.HasOne(me => me.Modelo)
            .WithMany(m => m.ExtrasDisponibles)
            .HasForeignKey(me => me.ModeloId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(me => me.Caracteristica)
            .WithMany(c => c.ModelosExtra)
            .HasForeignKey(me => me.CaracteristicaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}