namespace SAV.Contracts.Dtos;


    public class LoginDto
    {
        public string Email { get; set; } = null!;       
        public string Password { get; set; } = null!;
    }

    public class RegistrarAdministradorDto
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class RegistrarVendedorDto
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Nif { get; set; } = null!;
        public string? Domicilio { get; set; }
        public string? Telefono { get; set; }
    }

    public class RegistrarClienteDto
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Nif { get; set; } = null!;
        public string? Domicilio { get; set; }
        public string? Telefono { get; set; }
    }
