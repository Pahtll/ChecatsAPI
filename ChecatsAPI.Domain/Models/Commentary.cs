namespace ChecatsAPI.Domain.Models;

/// <summary>
/// Ушёл от этих доменных моделей в процессе разработки
/// </summary>
public class Commentary
{

    /// <summary>
    /// Валидация данных комментария
    /// </summary>
    /// <param name="content"></param>
    /// <returns>Возвращает доменную модель комментария</returns>
    /// <exception cref="ArgumentException"></exception>
    public static void Validate(string content)
    {
        if (string.IsNullOrEmpty(content))
            throw new ArgumentException("Commentary can not be null or empty");
    }
}