namespace SAV.Api.Responses
{
     public class RespuestaApi<T>
    {
        public bool Exito { get; set; }
        public string? Mensaje { get; set; }
        public T? Datos { get; set; }
    }
}
