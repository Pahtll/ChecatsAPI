using Checats.Application.Interfaces;
using Checats.Persistence;
using Checats.Persistence.Entities;
using ChecatsAPI.Domain.Abstractions;
using ChecatsAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Checats.Application.Services;

public class PostService(ChecatsDbContext context, IUserService userService) : IPostService
{
    /// <summary>
    /// Метод возвращающий список всех постов
    /// </summary>
    /// <returns>Возвращает в виде списка доменных моделей</returns>
    public async Task<List<PostEntity>> GetAllPosts()
    {
        return await context.Posts
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Метод возвращающий пост по его Ай-ди в БД
    /// </summary>
    /// <param name="id">Ай-ди поста</param>
    /// <returns>Возвращает его доменную модель</returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<PostEntity> GetPostById(Guid id)
    {
        return await context.Posts
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new ArgumentException("Post does not exist");
    }

    /// <summary>
    /// Метод возвращающий список постов конкретного пользователя
    /// </summary>
    /// <param name="authorId">Ай-ди пользователя, чьи посты нужно получить</param>
    /// <returns>Возвращает список доменных моделей постов</returns>
    public async Task<List<PostEntity>> GetAllUserPosts(Guid authorId)
    {
        var users = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == authorId)
                ?? throw new ArgumentException("User does not exist");

        return users.Posts!;
    }

    /// <summary>
    /// Метод для получения поста по его названию
    /// </summary>
    /// <param name="title">Название искомого поста</param>
    /// <returns>Возвращает его доменную модель</returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<PostEntity> GetPostByTitle(string title)
    {
        return await context.Posts
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Title == title)
                ?? throw new ArgumentException("Post does not exist");   
    }

    /// <summary>
    /// Метод создания постов, инициализирует его в БД, и привязывает к автору
    /// </summary>
    /// <param name="authorId">Ай-ди автора</param>
    /// <param name="title">Название поста</param>
    /// <param name="content">Контент наполнение поста</param>
    /// <returns>Возвращает ай-ди созданного пост</returns>
    public async Task<Guid> CreatePost(Guid authorId, string title, string content)
    {
        Post.Validate(title, content);
        _ = await context.Users
            .FirstOrDefaultAsync(u => u.Id == authorId && u.UserRole == UserRole.Admin)
                ?? throw new ArgumentException("User does not exist or don't have permissions for post creating");


        var postEntity = new PostEntity
        {
            Id = Guid.NewGuid(),
            Title = title,
            Content = content,
            Author = await userService.GetById(authorId),
        };

        context.Posts.Add(postEntity);
        await context.SaveChangesAsync();



        return postEntity.Id;
    }

    /// <summary>
    /// Метод обновления поста
    /// </summary>
    /// <param name="id">Его ай-ди</param>
    /// <param name="title">Новое / старое название</param>
    /// <param name="content">Новое / старое контент наполнение</param>
    /// <returns>Возвращает Ай-ди поста</returns>
    public async Task<Guid> UpdatePost(Guid id, string title, string content)
    {
        await context.Posts
            .AsNoTracking()
            .Where(p => p.Id == id)
            .ExecuteUpdateAsync(p =>
                p.SetProperty(pe => pe.Title, title)
                    .SetProperty(pe => pe.Content, content));

        return id;
    }

    /// <summary>
    /// Это чёрный ящик. Лучше не открывать
    /// Вообще метод реализует удаление поста из БД
    /// </summary>
    /// <param name="id">Дай бог по нему удалиться пост</param>
    /// <returns>Она вернётся, верим</returns>
    public async Task<Guid> DeletePost(Guid id)
    {
        await context.Posts
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}