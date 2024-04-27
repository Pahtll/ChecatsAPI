namespace ChecatsAPI.Domain.Abstractions;

/// <summary>
/// Енам, отвечает за уровень доступа пользователя
/// 0 - юзер
/// 1 - админ
/// </summary>
public enum UserRole
{
    User = 0,
    Admin = 1
}