namespace SAV.Infrastructure.Persistence.Configurations;

public class VentaConfiguration : IEntityTypeConfiguration<Venta>
{
    public void Configure(EntityTypeBuilder<Venta> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.PrecioCobrado)
            .HasColumnType("decimal(12,2)");

        builder.Property(v => v.FormaPago)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(v => v.Origen)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(v => v.Financiera)
            .HasMaxLength(100);

        builder.Property(v => v.Matricula)
            .HasMaxLength(20);

        builder.HasOne(v => v.Automovil)
            .WithOne(a => a.Venta)
            .HasForeignKey<Venta>(v => v.AutomovilId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.Cliente)
            .WithMany(c => c.Ventas)
            .HasForeignKey(v => v.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(v => v.Vendedor)
            .WithMany(ve => ve.Ventas)
            .HasForeignKey(v => v.VendedorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(v => v.ServicioOficial)
            .WithMany(s => s.Ventas)
            .HasForeignKey(v => v.ServicioOficialId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(v => v.PedidoFabrica)
            .WithMany(p => p.VentasOriginadas)
            .HasForeignKey(v => v.PedidoFabricaId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}