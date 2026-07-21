namespace SAV.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string mensaje) : base(mensaje) { }
    }
}