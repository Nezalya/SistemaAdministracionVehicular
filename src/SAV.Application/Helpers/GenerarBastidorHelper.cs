namespace SAV.Application.Helpers;
public static class GenerarBastidorHelper
{
    public static string GenerarBastidor()
    {
        return ("SAV" + Guid.NewGuid().ToString("N"))
            .Substring(0, 17)
            .ToUpper();
    }
}