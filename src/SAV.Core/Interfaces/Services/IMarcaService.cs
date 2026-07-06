namespace SAV.Core.Interfaces.Services
{
    public interface IMarcaService
    {
        Task<IEnumerable<Marca>> ObtenerTodasLasMarcas();
        Task<Marca?> ObtenerMarcaPorId(int id);
        Task<Marca> CrearMarca(Marca marca);
        Task<Marca> ActualizarMarca(int id, Marca marca);
        Task EliminarMarca(int id);
    }
}