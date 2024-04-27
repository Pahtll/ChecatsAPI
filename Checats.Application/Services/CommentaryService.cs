using Checats.Application.Interfaces;
using Checats.Persistence;
using Checats.Persistence.Entities;
using ChecatsAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Checats.Application.Services;

public class CommentaryService(ChecatsDbContext context, IUserService userService, IPostService postService) : ICommentaryService
{
    /// <summary>
    /// Метод для получения всех комментариев, которые есть в БД
    /// </summary>
    /// <returns>Возвращает список доменных моделей</returns>
    public async Task<List<CommentaryEntity>> GetAllCommentaries()
    {
        return await context.Commentaries
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Получение комментария по его Ай-ди
    /// </summary>
    /// <param name="id">Ай-ди искомого комментария</param>
    /// <returns>Возвращает доменную модель этого комментария</returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<CommentaryEntity> GetCommentaryById(Guid id)
    {
        return await context.Commentaries
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new ArgumentException("Commentary does not exist");

    }

    /// <summary>
    /// Метод для получения всех комментариев конкретного пользователя
    /// </summary>
    /// <param name="authorId">Ай-ди автора комментариев</param>
    /// <returns>Возвращает список доменных моделей комментариев</returns>
    public async Task<List<CommentaryEntity>?> GetAllUserCommentaries(Guid authorId)
    {
        var author = await context.Users
            .FirstOrDefaultAsync(u => u.Id == authorId)
                ?? throw new ArgumentException("User does not exist");

        return author.Commentaries;

    }

    /// <summary>
    /// Метод для получения списка всех комментариев под постом
    /// </summary>
    /// <param name="postId">Ай-ди поста по которому ищем комментарии</param>
    /// <returns>Возвращает доменную модель комментария</returns>
    public async Task<List<CommentaryEntity>?> GetAllPostCommentaries(Guid postId)
    {
        var post = await context.Posts
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == postId)
                ?? throw new ArgumentException("Post does not exist");

        return post.Commentaries;
    }

    /// <summary>
    /// Метод для создания нового комментария
    /// </summary>
    /// <param name="authorId">Ай-ди атора</param>
    /// <param name="postId">Ай-ди поста</param>
    /// <param name="content">Его контент наполнение</param>
    /// <returns>Возвращает Ай-ди созданного комментария</returns>
    public async Task<Guid> CreateCommentary(Guid authorId, Guid postId, string content)
    {
        Commentary.Validate(content);

        var commentaryEntity = new CommentaryEntity
        {
            Id = Guid.NewGuid(),
            Content = content,
            Author = await userService.GetById(authorId),
            Post = await postService.GetPostById(postId)
        };

        await context.Commentaries.AddAsync(commentaryEntity);
        await context.SaveChangesAsync();

        return commentaryEntity.Id;
    }

    /// <summary>
    /// Метод для удаления комментария
    /// </summary>
    /// <param name="id">Ай-ди комментария, который требуется удалить</param>
    /// <returns>Возвращает его же Ай-ди, который теперь свободен</returns>
    public async Task<Guid> DeleteCommentary(Guid id)
    {
        await context.Commentaries
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    /// <summary>
    /// Изменения в комментарии
    /// </summary>
    /// <param name="id">Ай-ди комментария</param>
    /// <param name="content">Его новое контент наполнение</param>
    /// <returns>Возвращает Ай-ди комментария</returns>
    public async Task<Guid> CommentaryUpdate(Guid id, string content)
    {
        await context.Commentaries
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(up =>
                up
                    .SetProperty(ue => ue.Content, content));

        return id;
    }
}