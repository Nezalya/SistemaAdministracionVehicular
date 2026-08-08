using AutoMapper;
namespace SAV.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ---- Catálogo ----
            CreateMap<Marca, MarcaDto>();

            CreateMap<Modelo, ModeloDto>()
                .ForMember(d => d.MarcaNombre,
                           options => options.MapFrom(s => s.Marca != null ? s.Marca.Nombre : null));

            CreateMap<Caracteristica, CaracteristicaDto>();

            // ---- Cliente ----
            CreateMap<Cliente, ClienteDto>();
            CreateMap<GuardarClienteDto, Cliente>();

            // ---- Vendedor ----
            CreateMap<Vendedor, VendedorDto>();
            CreateMap<GuardarVendedorDto, Vendedor>();

            // ---- Concesionario ----
            CreateMap<Concesionario, ConcesionarioDto>();
            CreateMap<GuardarConcesionarioDto, Concesionario>();

            // ---- ServicioOficial ----
            CreateMap<ServicioOficial, ServicioOficialDto>();
            CreateMap<GuardarServicioOficialDto, ServicioOficial>();

            // ---- Automovil ----
            CreateMap<Automovil, AutomovilDto>()
    .ForMember(d => d.ModeloNombre,
        o => o.MapFrom(s => s.Modelo != null
            ? s.Modelo.Nombre
            : null))

    .ForMember(d => d.MarcaNombre,
        o => o.MapFrom(s => s.Modelo != null && s.Modelo.Marca != null
            ? s.Modelo.Marca.Nombre
            : null))

    .ForMember(d => d.Ubicacion,
        o => o.MapFrom(s => s.ServicioOficialUbicacion != null
            ? s.ServicioOficialUbicacion.Nombre
            : s.ConcesionarioUbicacion != null
                ? s.ConcesionarioUbicacion.Nombre
                : null))

        .ForMember(d => d.UbicacionTipo,
        o => o.MapFrom(s => s.ServicioOficialUbicacionId.HasValue
            ? TipoUbicacion.ServicioOficial
            : TipoUbicacion.Concesionario));

            // ---- Venta ----
            // El precio base se deduce restando los extras al total cobrado: la venta
            // guarda el importe final, no el desglose.
            CreateMap<Venta, VentaDto>()
                .ForMember(d => d.PrecioTotal, o => o.MapFrom(s => s.PrecioCobrado))
                .ForMember(d => d.TotalExtras, o => o.MapFrom(s => s.Extras.Sum(e => e.PrecioCobrado)))
                .ForMember(d => d.PrecioBase,
                           o => o.MapFrom(s => s.PrecioCobrado - s.Extras.Sum(e => e.PrecioCobrado)))
                .ForMember(d => d.ClienteNombre,
                           o => o.MapFrom(s => s.Cliente != null ? s.Cliente.Nombre : null))
                .ForMember(d => d.VendedorNombre,
                           o => o.MapFrom(s => s.Vendedor != null ? s.Vendedor.Nombre : null))
                .ForMember(d => d.ServicioOficialNombre,
                           o => o.MapFrom(s => s.ServicioOficial != null ? s.ServicioOficial.Nombre : null))
                .ForMember(d => d.AutomovilBastidor,
                           o => o.MapFrom(s => s.Automovil != null ? s.Automovil.NumBastidor : null))
                .ForMember(d => d.AutomovilModelo,
                           o => o.MapFrom(s => s.Automovil != null && s.Automovil.Modelo != null
                               ? s.Automovil.Modelo.Nombre
                               : null))
                .ForMember(d => d.AutomovilMarca,
                           o => o.MapFrom(s => s.Automovil != null && s.Automovil.Modelo != null
                                               && s.Automovil.Modelo.Marca != null
                               ? s.Automovil.Modelo.Marca.Nombre
                               : null));

            CreateMap<VentaExtra, VentaExtraDto>()
                .ForMember(d => d.Nombre,
                           o => o.MapFrom(s => s.Caracteristica != null ? s.Caracteristica.Nombre : null));

            CreateMap<CrearVentaDto, Venta>()
                .ForMember(d => d.Extras, options => options.Ignore());

            // ---- Automóvil (alta) ----
            CreateMap<RegistrarAutomovilDto, Automovil>();

            // ---- Usuario ----
            CreateMap<Usuario, UsuarioDto>();



            CreateMap<ModeloExtra, ExtraDisponibleDto>()
                .ForMember(d => d.Nombre,
               o => o.MapFrom(s => s.Caracteristica != null ? s.Caracteristica.Nombre : null));

            CreateMap<ActualizarAutomovilDto, Automovil>();

            
            CreateMap<ModeloSerie, EquipamientoSerieDto>()
                .ForMember(d => d.Nombre,
                           o => o.MapFrom(s => s.Caracteristica != null ? s.Caracteristica.Nombre : null))
                .ForMember(d => d.Categoria,
                           o => o.MapFrom(s => s.Caracteristica != null ? s.Caracteristica.Categoria : null));

            
            CreateMap<Modelo, ConfiguracionModeloDto>()
                .ForMember(d => d.ModeloId, o => o.MapFrom(s => s.Id));

            CreateMap<RegistrarVendedorDto, Vendedor>();
            CreateMap<RegistrarClienteDto, Cliente>();
            CreateMap<Usuario, UsuarioRegistradoDto>()
    .ForMember(d => d.Rol, o => o.MapFrom(s => s.Rol.ToString()));
        }
    }
}