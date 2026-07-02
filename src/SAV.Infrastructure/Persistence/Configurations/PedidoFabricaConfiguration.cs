namespace SAV.Infrastructure.Persistence.Configurations;

public class PedidoFabricaConfiguration : IEntityTypeConfiguration<PedidoFabrica>
{
    public void Configure(EntityTypeBuilder<PedidoFabrica> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Estado)
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(p => p.TotalPedido)
            .HasColumnType("decimal(12,2)");

        builder.HasOne(p => p.Concesionario)
            .WithMany(c => c.Pedidos)
            .HasForeignKey(p => p.ConcesionarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}