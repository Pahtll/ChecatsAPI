namespace Checats.Infrastructure;

public class PasswordHasher : IPasswordHasher
{
    /// <summary>
    /// Метод для Хеширования пароля
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public string GenerateHash(string password)
        => BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    
    /// <summary>
    /// Метод для сравнения поступившего пароля и хэша из БД
    /// </summary>
    /// <param name="password">Поступивший пароль</param>
    /// <param name="hash">Хэш пароля из БД</param>
    /// <returns></returns>
    public bool VerifyHash(string password, string hash)
        => BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
}