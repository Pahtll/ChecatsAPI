namespace Checats.Infrastructure;

public class JwtOptions
{
    /// <summary>
    /// Секретный ключ для шифрования токена, берётся из appsettings.json 
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;
    
    /// <summary>
    /// Время, которое токен будет действителен
    /// </summary>
    public int ExpiresHours { get; set; }
}