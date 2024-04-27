using Checats.Application.Interfaces;
using Checats.Infrastructure;
using Checats.Persistence;
using Checats.Persistence.Entities;
using ChecatsAPI.Domain.Abstractions;
using ChecatsAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Checats.Application.Services;

/// <summary>
/// Сервис, который реализует методы для работы с таблицей пользователей в БД
/// </summary>
/// <param name="jwtProvider">Отвечает за процесс аутентификации</param>
/// <param name="passwordHasher">Отвечает за хеширование пароля</param>
/// <param name="context">Контекст нашей БД</param>
public class UserService(IJwtProvider jwtProvider, IPasswordHasher passwordHasher, ChecatsDbContext context) : IUserService
{
    /// <summary>
    /// Метод сервиса по аутентификации пользователя, проверяет его пароль и логин
    /// </summary>
    /// <param name="name">Логин по которому происходит вход пользователя</param>
    /// <param name="password">Пароль для входа в аккаунт</param>
    /// <returns>Возвращает токен, который позже будет помещён в куки файлы</returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<string> Login(string name, string password)
    {
        var userEntity = await GetByName(name);

        if (!passwordHasher.VerifyHash(password, userEntity.PasswordHash!))
            throw new ArgumentException("Incorrect password");

        var token = jwtProvider.GenerateToken(userEntity);

        return token;
    }

    /// <summary>
    /// Получение сущности пользователя по его имени
    /// </summary>
    /// <param name="name">Имя пользователя</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<UserEntity> GetByName(string name)
    {
        var userEntity = await context.Users
                             .AsNoTracking().Include(userEntity => userEntity.Posts)
                             .Include(userEntity => userEntity.Commentaries)
                             .FirstOrDefaultAsync(u => u.Username == name)
                ?? throw new ArgumentException("User does not exist");

        return userEntity;
    }

    /// <summary>
    /// Получение сущности пользователя по его ай-ди
    /// </summary>
    /// <param name="id">Ай-ди пользователя</param>
    /// <returns>Возвращает доменную модель пользователя</returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<UserEntity> GetById(Guid id)
    {
        var userEntity = await context.Users
                             .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new ArgumentException("User does not exist");

        return userEntity;
    }

    /// <summary>
    /// Получение пользователя по его Email
    /// </summary>
    /// <param name="email">Электронная почта пользователя указанная при рекгистрации</param>
    /// <returns>Возвращает доменную модель пользователя</returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<UserEntity> GetByEmail(string email)
    {
        var userEntity = await context.Users
                             .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new ArgumentException("User does not exist");


        return userEntity;
    }

    /// <summary>
    /// Метод для получение списка всех пользователей
    /// </summary>
    /// <returns>Возвращает список доменных моделей пользователей</returns>
    public async Task<List<UserEntity>> GetAll()
    {
        var users = await context.Users
            .AsNoTracking()
            .ToListAsync();

        return users;
    }

    /// <summary>
    /// Метод для регистрации пользователя
    /// </summary>
    /// <param name="username">Его имя пользователя, которое в последствии будет использоваться для входа</param>
    /// <param name="email">Его электронная почта, которая нужна для верификации аккаунта</param>
    /// <param name="password">Пароль, который позже в хэшированном виде будет храниться в БД</param>
    /// <returns>Возвращает ай-ди зарегестрированного пользователя</returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<Guid> Register(string username, string email, string password, UserRole userRole = UserRole.User)
    {
        User.Validate(username, email, password);

        var passwordHash = passwordHasher.GenerateHash(password);

        var userEntity = new UserEntity
        {
            Id = Guid.NewGuid(),
            Username = username,
            Email = email,
            PasswordHash = passwordHash,
            UserRole = userRole
        };

        context.Users.Add(userEntity);
        await context.SaveChangesAsync();

        return userEntity.Id;
    }

    /// <summary>
    /// Метод для удаления пользователя по его ай-ди
    /// </summary>
    /// <param name="id">Уникальный Ай-ди пользователя</param>
    /// <returns>Возвращает так же его Ай-ди</returns>
    public async Task<Guid> DeleteUser(Guid id)
    {
        await context.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    /// <summary>
    /// Изменение роли пользователя
    /// </summary>
    /// <param name="id">Ай-ди пользователя, которого нужно попустить / поднять</param>
    /// <param name="userRole">Роль, которую надлежит присвоить</param>
    /// <returns></returns>
    public async Task<Guid> ChangeUserRole(Guid id, UserRole userRole)
    {
        await context.Users
            .AsNoTracking()
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(u =>
                u.SetProperty(ue => ue.UserRole, userRole));

        return id;
    }

    /// <summary>
    /// Метод для изменения аватарки в профиле
    /// </summary>
    /// <param name="id">Ай-ди пользователя, чью аватарку нужно изменить</param>
    /// <param name="profilePicture">Новая аватарака</param>
    /// <returns>Возвращает ай-ди пользователя</returns>
    public async Task<Guid> ChangeProfilePicture(Guid id, byte[] profilePicture)
    {
        await context.Users
            .AsNoTracking()
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(u =>
                u.SetProperty(ue => ue.ProfilePicture, profilePicture));

        return id;
    }

    /// <summary>
    /// Метод для изменения пароля в БД, новый пароль так же хэшируется и записывается в БД
    /// </summary>
    /// <param name="id">Ай-ди пользователя для изменения пароля</param>
    /// <param name="password">Новый пароль</param>
    /// <returns></returns>
    public async Task<Guid> ChangePassword(Guid id, string oldPassword, string newPassword)
    {
        if (oldPassword == null || newPassword == null)
            throw new ArgumentNullException("Password can not be null");

        await context.Users
            .AsNoTracking()
            .Where(u =>
                u.Id == id && passwordHasher.VerifyHash(u.PasswordHash, oldPassword))
            .ExecuteUpdateAsync(up =>
                up.SetProperty(ue => ue.PasswordHash, passwordHasher.GenerateHash(newPassword)));

        return id;
    }

    /// <summary>
    /// Метод для смены email адреса по Ай-ди пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="email">Новый адрес электронной почты</param>
    /// <returns>Возвращает ай-ди пользователя</returns>
    public async Task<Guid> ChangeEmail(Guid id, string email)
    {
        await context.Users
            .AsNoTracking()
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(u =>
                u.SetProperty(ue => ue.Email, email));

        return id;
    }

    /// <summary>
    /// Метод обновляет сущность пользователя в БД
    /// </summary>
    /// <param name="id">Его Ай-ди по которому происходит поиск в БД</param>
    /// <param name="username">Новое / старое Имя пользователя</param>
    /// <param name="email">Его новый / старый адрес электронной почты</param>
    /// <param name="password">Его новый / старый пароль</param>
    /// <param name="userRole">Его новая / старая роль</param>
    /// <param name="posts">Его список постов</param>
    /// <param name="commentaries">Список его комментариев</param>
    /// <param name="profilePicture">Его аватарка</param>
    /// <returns>Возвращает Ай-ди этого пользователя</returns>
    public async Task<Guid> UpdateUser(Guid id, string username, string email, string password,
        UserRole userRole = UserRole.User, byte[]? profilePicture = null)
    {
        if (username == null || email == null)
            throw new ArgumentNullException("Email or username can not be null");

        if (password == null)
            throw new ArgumentException("Password can not be null");

        await context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(up =>
                up.SetProperty(u => u.Username, username)
                    .SetProperty(u => u.Email, email)
                    .SetProperty(u => u.PasswordHash, passwordHasher.GenerateHash(password))
                    .SetProperty(u => u.UserRole, userRole)
                    .SetProperty(u => u.ProfilePicture, profilePicture));

        return id;
    }
}