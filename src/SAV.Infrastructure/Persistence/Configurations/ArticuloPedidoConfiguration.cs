namespace SAV.Infrastructure.Persistence.Configurations;

public class ArticuloPedidoConfiguration : IEntityTypeConfiguration<ArticuloPedido>
{
    public void Configure(EntityTypeBuilder<ArticuloPedido> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.TipoArticulo)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(a => a.PrecioUnitario)
            .HasColumnType("decimal(12,2)");

        builder.HasOne(a => a.Pedido)
            .WithMany(p => p.Articulos)
            .HasForeignKey(a => a.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Modelo)
            .WithMany(m => m.ArticulosPedido)
            .HasForeignKey(a => a.ModeloId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.Caracteristica)
            .WithMany(c => c.ArticulosPedido)
            .HasForeignKey(a => a.CaracteristicaId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}