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
            return await _modeloRepository.ObtenerModelosConMarca();  
        }

        public async Task<Modelo?> ObtenerModeloPorId(int id)
        {
            return await _modeloRepository.ObtenerModeloConMarca(id);     
        }

        public async Task<IEnumerable<ModeloExtra>?> ObtenerExtrasDeModelo(int id)
        {
            var modelo = await _modeloRepository.ObtenerModeloConExtras(id);
            if (modelo is null) return null;          
            return modelo.ExtrasDisponibles;         
        }
    }
}
