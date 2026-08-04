namespace SAV.Application.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string mensaje) : base(mensaje) { }
    }
}