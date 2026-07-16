namespace SAV.Core.Interfaces.Repositories
{
    public interface IModeloRepository : IRepository<Modelo>
    {
        Task<IEnumerable<Modelo>> ObtenerModelosConMarca();
        Task<Modelo?> ObtenerModeloConMarca(int id);
        Task<Modelo?> ObtenerModeloConExtras(int id);
    }
}
