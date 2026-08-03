namespace SAV.Application.Interfaces.Services
{
    public interface IConcesionarioService
    {
        Task<IEnumerable<Concesionario>> ObtenerElConcesionario();
        Task<Concesionario?> ObtenerConcesionarioPorId(int id);
        Task<Concesionario> CrearConcesionario(Concesionario concesionario);
        Task<Concesionario> ActualizarConcesionario(int id, Concesionario concesionario);
    }
}
