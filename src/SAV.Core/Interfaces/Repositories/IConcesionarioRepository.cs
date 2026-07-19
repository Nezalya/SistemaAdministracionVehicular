namespace SAV.Core.Interfaces.Repositories
{
    public interface IConcesionarioRepository : IRepository<Concesionario>
    {
        Task<Concesionario?> ObtenerConcesionarioPorNif(string nif);
    }
}