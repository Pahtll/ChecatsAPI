using ChecatsAPI.Domain.Abstractions;

namespace Checats.Persistence.Entities;

public class UserEntity
{
    /// <summary>
    /// Ай-ди сущности пользователя, выступает ключом для хранения в БД
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя пользователя, должно быть уникальным
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Почта, используется для регистрации и подтверждения аккаунта пользователя
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Хэш пароля, который храниться в БД
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    /// Роль пользователя, реализована через Енам 0 - пользователь, 1 - админ
    /// </summary>
    public UserRole UserRole { get; set; }

    /// <summary>
    /// Список Ай-дишников постов, написанных пользователем
    /// </summary>
    public List<PostEntity>? Posts { get; set; }

    /// <summary>
    /// Список комментариев оставленных пользователелем
    /// </summary>
    public List<CommentaryEntity>? Commentaries { get; set; }

    /// <summary>
    /// Список байтов или аватарка пользователя
    /// </summary>
    public byte[]? ProfilePicture { get; set; }
}
