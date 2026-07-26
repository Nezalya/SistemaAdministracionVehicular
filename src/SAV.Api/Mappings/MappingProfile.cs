using SAV.Core.DTOs;

namespace SAV.Api.Mappings
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
                           options => options.MapFrom(s => s.Modelo != null ? s.Modelo.Nombre : null));
            CreateMap<RegistrarAutomovilDto, Automovil>();

            // ---- Venta ----
            CreateMap<Venta, VentaDto>();
            CreateMap<VentaExtra, VentaExtraDto>();
            CreateMap<CrearVentaDto, Venta>()
                .ForMember(d => d.Extras, options => options.Ignore()); 

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

            CreateMap<Venta, VentaDto>()
    .ForMember(d => d.PrecioTotal, o => o.MapFrom(s => s.PrecioCobrado))
    .ForMember(d => d.TotalExtras, o => o.MapFrom(s => s.Extras.Sum(e => e.PrecioCobrado)))
    .ForMember(d => d.PrecioBase, o => o.MapFrom(s => s.PrecioCobrado - s.Extras.Sum(e => e.PrecioCobrado)));

            
            CreateMap<VentaExtra, VentaExtraDto>()
                .ForMember(d => d.Nombre,
                           o => o.MapFrom(s => s.Caracteristica != null ? s.Caracteristica.Nombre : null));
            CreateMap<RegistrarVendedorDto, Vendedor>();
            CreateMap<RegistrarClienteDto, Cliente>();
            CreateMap<Usuario, UsuarioRegistradoDto>()
    .ForMember(d => d.Rol, o => o.MapFrom(s => s.Rol.ToString()));
        }
    }
}