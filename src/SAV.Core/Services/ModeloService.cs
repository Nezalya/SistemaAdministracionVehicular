namespace SAV.Core.Services
{
    public class ModeloService: IModeloService
    {
        private readonly IModeloRepository _modeloRepository;
        public ModeloService(IModeloRepository modeloRepository) 
        { 
        _modeloRepository = modeloRepository;
        }
        public async Task<IEnumerable<Modelo>> ObtenerTodosLosModelos()
        {
            return await _modeloRepository.GetAll();
        }

        public async Task<Modelo?> ObtenerModeloPorId(int id)
        {
            return await _modeloRepository.GetById(id);
        }
    }
}
