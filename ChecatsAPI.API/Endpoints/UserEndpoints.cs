using Checats.Application.Interfaces;
using ChecatsAPI.Domain.Abstractions;
using ChecatsAPI.Traits;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChecatsAPI.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("register", Register);
        app.MapPost("login", Login);
        app.MapGet("GetAllUsers", GetAllUsers);
        app.MapGet("GetUserByName", GetUserByName);
        app.MapGet("GetUserById", GetUserById);
        app.MapDelete("DeleteUser", DeleteUser);
        app.MapPut("ChangeProfilePicture", ChangeProfilePicture);
        app.MapPut("ChangeUserEmail", ChangeUserEmail);
        app.MapPut("ChangeUserRole", ChangeUserRole);
        app.MapPut("ChangeUserPassword", ChangeUserPassword);
        

        return app;
    }

    /// <summary>
    /// Эндпоинт, который отвечает за регистрацию пользователя
    /// </summary>
    /// <param name="registerRequest"></param>
    /// <param name="userService"></param>
    /// <returns></returns>
    private static async Task<IResult> Register(RegisterRequest registerRequest, IUserService userService)
    {
        var userId =
            await userService.Register(registerRequest.Username, registerRequest.Email, registerRequest.Password);
        
        return Results.Ok(userId);
    }

    /// <summary>
    /// Эндпоинт, который отвечает за вход пользователя в систему
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <param name="userService"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private static async Task<IResult> Login(LoginRequest loginRequest, IUserService userService, HttpContext httpContext)
    {
        var token = await userService.Login(loginRequest.Username, loginRequest.Password);
        
        httpContext.Response.Cookies.Append("ToDoSomethingCookies", token);
        
        return Results.Ok(token);
    }

    /// <summary>
    /// Эндпоинт, который возвращает список всех пользователей
    /// </summary>
    /// <param name="userService"></param>
    /// <returns></returns>
    private static async Task<IResult> GetAllUsers(IUserService userService)
    {
        return Results.Ok(await userService.GetAll());
    }
    
    /// <summary>
    /// Эндпоинты, который возвращает пользователя по его ай-ди
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userService"></param>
    /// <returns></returns>
    private static async Task<IResult> GetUserById(Guid id, IUserService userService)
    {
        try
        {
            var user = await userService.GetById(id);
            return Results.Ok(user);
        }
        catch (Exception e)
        {
           return Results.BadRequest(e.ToString());
        }
    }

    /// <summary>
    /// Эндпоинт, который возвращает пользователя по его имени
    /// </summary>
    /// <param name="name"></param>
    /// <param name="userService"></param>
    /// <returns></returns>
    private static async Task<IResult> GetUserByName(string name, IUserService userService)
    {
        try
        {
            var user = await userService.GetByName(name);
            return Results.Ok(user);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.ToString());
        }
    }
    
    /// <summary>
    /// Эндпоинт для удаления пользователей
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userService"></param>
    /// <returns></returns>
    private static async Task<IResult> DeleteUser(Guid id, IUserService userService)
    {
        try
        {
            var userId = await userService.DeleteUser(id);
            return Results.Ok(userId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.ToString());
        }
    }

    /// <summary>
    /// Эндпоинт для изменения роли пользователя
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userRole"></param>
    /// <param name="userService"></param>
    /// <returns></returns>
    private static async Task<IResult> ChangeUserRole(Guid id, UserRole userRole, IUserService userService)
    {
        try
        {
            var userId = await userService.ChangeUserRole(id, userRole);
            return Results.Ok(userId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.ToString());
        }
    }
    
    /// <summary>
    /// Эндпоинт для изменения пароля пользователя
    /// </summary>
    /// <param name="id"></param>
    /// <param name="password"></param>
    /// <param name="userService"></param>
    /// <returns></returns>
    private static async Task<IResult> ChangeUserPassword(Guid id, string oldPassword, string password, IUserService userService)
    {
        try
        {
            var userId = await userService.ChangePassword(id, oldPassword, password);
            return Results.Ok(userId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.ToString());
        }
    }
    
    /// <summary>
    /// Эндпоинт для изменения почты пользователя
    /// </summary>
    /// <param name="id"></param>
    /// <param name="email"></param>
    /// <param name="userService"></param>
    /// <returns></returns>
    private static async Task<IResult> ChangeUserEmail(Guid id, string email, IUserService userService)
    {
        try
        {
            var userId = await userService.ChangeEmail(id, email);
            return Results.Ok(userId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.ToString());
        }
    }
    
    /// <summary>
    /// Эндпоинт для изменения аватара пользователя
    /// </summary>
    /// <param name="id"></param>
    /// <param name="profilePicture"></param>
    /// <param name="userService"></param>
    /// <returns></returns>
    private static async Task<IResult> ChangeProfilePicture(Guid id, byte[] profilePicture, IUserService userService)
    {
        try
        {
            var userId = await userService.ChangeProfilePicture(id, profilePicture);
            return Results.Ok(userId);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.ToString());
        }
    }
}