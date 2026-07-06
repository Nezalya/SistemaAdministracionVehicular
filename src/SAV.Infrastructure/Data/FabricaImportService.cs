namespace SAV.Infrastructure.Data
{
    public class FabricaImportService : IFabricaImportService
    {
        private readonly SavDbContext _context;

        public FabricaImportService(SavDbContext context)
        {
            _context = context;
        }

        public async Task<string> Importar()
        {
            // 1. Evitar doble importación: si ya hay marcas, no volvemos a cargar
            if (_context.Marcas.Any())
                return "El catálogo ya fue importado anteriormente. No se hizo nada.";

            // 2. Ubicar y leer el archivo (se copió a la carpeta de salida)
            var ruta = Path.Combine(AppContext.BaseDirectory, "Data", "Fabrica.json");

            if (!File.Exists(ruta))
                throw new FileNotFoundException($"No se encontró el archivo de fábrica en: {ruta}");

            var json = await File.ReadAllTextAsync(ruta);

            // 3. Deserializar (camelCase del JSON -> PascalCase de C#)
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<FabricaArchivoDto>(json, opciones);

            if (data is null)
                throw new InvalidOperationException("El archivo de fábrica está vacío o mal formado.");

            // 4. Crear las Características y guardarlas en un diccionario por código
            //    (así los modelos las pueden referenciar al armar serie/extras)
            var mapaCaracteristicas = new Dictionary<string, Caracteristica>();

            foreach (var cDto in data.Caracteristicas)
            {
                var caracteristica = new Caracteristica
                {
                    Nombre = cDto.Nombre,
                    Descripcion = cDto.Descripcion,
                    Categoria = cDto.Categoria
                };
                mapaCaracteristicas[cDto.Codigo] = caracteristica;
                _context.Caracteristicas.Add(caracteristica);
            }

            // 5. Crear Marcas -> Modelos -> Serie/Extras usando las navegaciones
            int totalModelos = 0;

            foreach (var mDto in data.Marcas)
            {
                var marca = new Marca
                {
                    Nombre = mDto.Nombre,
                    PaisOrigen = mDto.PaisOrigen
                };

                foreach (var moDto in mDto.Modelos)
                {
                    var modelo = new Modelo
                    {
                        Nombre = moDto.Nombre,
                        PrecioBase = moDto.PrecioBase,
                        Descuento = moDto.Descuento,
                        PotenciaFiscal = moDto.PotenciaFiscal,
                        Cilindrada = moDto.Cilindrada
                    };

                    // Equipamiento de serie
                    foreach (var codigo in moDto.EquipamientoSerie)
                    {
                        if (mapaCaracteristicas.TryGetValue(codigo, out var caracteristica))
                        {
                            modelo.EquipamientoDeSerie.Add(new ModeloSerie
                            {
                                Caracteristica = caracteristica
                            });
                        }
                    }

                    // Extras disponibles
                    foreach (var extraDto in moDto.ExtrasDisponibles)
                    {
                        if (mapaCaracteristicas.TryGetValue(extraDto.CodigoCaracteristica, out var caracteristica))
                        {
                            modelo.ExtrasDisponibles.Add(new ModeloExtra
                            {
                                Caracteristica = caracteristica,
                                PrecioExtra = extraDto.PrecioExtra
                            });
                        }
                    }

                    marca.Modelos.Add(modelo);
                    totalModelos++;
                }

                _context.Marcas.Add(marca);
            }

            // 6. Un solo SaveChanges: EF asigna todos los Id y resuelve las FKs
            await _context.SaveChangesAsync();

            return $"Importación completa: {data.Marcas.Count} marcas, " +
                   $"{totalModelos} modelos, {data.Caracteristicas.Count} características.";
        }
    }
}
