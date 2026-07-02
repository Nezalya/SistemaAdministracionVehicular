namespace SAV.Infrastructure.Persistence.Configurations;

public class VentaExtraConfiguration : IEntityTypeConfiguration<VentaExtra>
{
    public void Configure(EntityTypeBuilder<VentaExtra> builder)
    {
        builder.HasKey(ve => new { ve.VentaId, ve.CaracteristicaId });

        builder.Property(ve => ve.PrecioCobrado)
            .HasColumnType("decimal(12,2)");

        builder.HasOne(ve => ve.Venta)
            .WithMany(v => v.Extras)
            .HasForeignKey(ve => ve.VentaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ve => ve.Caracteristica)
            .WithMany(c => c.VentasExtra)
            .HasForeignKey(ve => ve.CaracteristicaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}