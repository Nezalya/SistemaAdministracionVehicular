namespace SAV.Core.Interfaces.Services
{
    public interface IMarcaService
    {
        Task<IEnumerable<Marca>> ObtenerTodasLasMarcas();
        Task<Marca?> ObtenerMarcaPorId(int id);
    }
}