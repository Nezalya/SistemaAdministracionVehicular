namespace SAV.Api.Authorization
{
    public static class Roles
    {
        public const string Administrador = "Administrador";
        public const string Vendedor = "Vendedor";
        public const string Cliente = "Cliente";//Dejarlo por si en un futuro la aplicacion cambia de rumbo
        public const string AdminOVendedor = "Administrador,Vendedor";
    }
}