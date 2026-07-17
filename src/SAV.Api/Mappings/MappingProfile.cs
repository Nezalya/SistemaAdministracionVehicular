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
            CreateMap<CrearUsuarioDto, Usuario>()
                .ForMember(d => d.PasswordHash, options => options.Ignore());


            CreateMap<ModeloExtra, ExtraDisponibleDto>()
                .ForMember(d => d.Nombre,
               o => o.MapFrom(s => s.Caracteristica != null ? s.Caracteristica.Nombre : null));

            CreateMap<ActualizarAutomovilDto, Automovil>();
        }
    }
}