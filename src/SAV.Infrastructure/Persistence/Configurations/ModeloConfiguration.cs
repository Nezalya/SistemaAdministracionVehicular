namespace SAV.Infrastructure.Persistence.Configurations;

public class ModeloConfiguration : IEntityTypeConfiguration<Modelo>
{
    public void Configure(EntityTypeBuilder<Modelo> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(m => m.PrecioBase)
            .HasColumnType("decimal(12,2)");

        builder.Property(m => m.Descuento)
            .HasColumnType("decimal(5,2)");

        builder.Property(m => m.PotenciaFiscal)
            .HasMaxLength(20);

        builder.Property(m => m.Cilindrada)
            .HasMaxLength(20);

        builder.HasOne(m => m.Marca)
            .WithMany(ma => ma.Modelos)
            .HasForeignKey(m => m.MarcaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
