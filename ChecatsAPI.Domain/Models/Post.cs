namespace ChecatsAPI.Domain.Models;

/// <summary>
/// Ушёл от этих доменных моделей в процессе разработки
/// </summary>
public class Post
{
    /// <summary>
    /// Валидация данных поста
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <returns>Возвращает доменную модель поста</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="AggregateException"></exception>
    public static void Validate(string title, string content)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentException("Title can not be null or empty");

        if (string.IsNullOrEmpty(content))
            throw new AggregateException("Content can not be null or empty");
    }
}