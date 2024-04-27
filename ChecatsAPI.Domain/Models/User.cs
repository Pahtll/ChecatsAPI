using System.Net.Mail;
using ChecatsAPI.Domain.Abstractions;

namespace ChecatsAPI.Domain.Models;

/// <summary>
/// Ушёл от этих доменных моделей в процессе разработки
/// </summary>
public class User
{
    /// <summary>
    /// Валидирует данные пользователя
    /// </summary>
    /// <param name="username">Имя пользователя</param>
    /// <param name="email">Почта</param>
    /// <param name="password">Пароль</param>
    /// <returns>
    /// Возвращает новый экземпляр доменной модели
    /// </returns>
    public static void Validate(string username, string email, string password)
    {
        if (string.IsNullOrEmpty(username))
            throw new ArgumentException("Username can not be null or empty");

        if (string.IsNullOrEmpty(email))
            throw new AggregateException("Email can not be null or empty");

        if (string.IsNullOrEmpty(password))
            throw new AggregateException("Password can not be null or empty");

        try
        {
            var mailAddress = new MailAddress(email).Address;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
