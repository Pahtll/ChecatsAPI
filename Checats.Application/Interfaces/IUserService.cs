using Checats.Persistence.Entities;
using ChecatsAPI.Domain.Abstractions;

namespace Checats.Application.Interfaces;

public interface IUserService
{
    Task<Guid> ChangeEmail(Guid id, string email);
    Task<Guid> ChangePassword(Guid id, string oldPassword, string newPassword);
    Task<Guid> ChangeProfilePicture(Guid id, byte[] profilePicture);
    Task<Guid> ChangeUserRole(Guid id, UserRole userRole);
    Task<Guid> DeleteUser(Guid id);
    Task<List<UserEntity>> GetAll();
    Task<UserEntity> GetByEmail(string email);
    Task<UserEntity> GetById(Guid id);
    Task<UserEntity> GetByName(string name);
    Task<string> Login(string name, string password);
    Task<Guid> Register(string username, string email, string password, UserRole userRole = UserRole.User);
    Task<Guid> UpdateUser(Guid id, string username, string email, string password, UserRole userRole = UserRole.User, byte[]? profilePicture = null);
}
