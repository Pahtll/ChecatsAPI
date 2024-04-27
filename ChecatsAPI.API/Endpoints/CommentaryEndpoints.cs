using Checats.Application.Interfaces;
using ChecatsAPI.Traits;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChecatsAPI.Endpoints;

public static class CommentaryEndpoints
{
    public static IEndpointRouteBuilder MapCommentaryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("GetAllCommentaries", GetAllCommentaries);
        app.MapGet("GetAllUserCommentaries", GetAllUserCommentaries);
        app.MapGet("GetAllPostCommentaries", GetAllPostCommentaries);
        app.MapPost("CreateCommentary", CreateCommentary);
        app.MapDelete("DeleteCommentary", DeleteCommentary);
        app.MapPut("UpdateCommentary", UpdateCommentary);

        return app;
    }

    /// <summary>
    /// Эндпоинт, который возвращает список из всех комментариев в базе данных
    /// </summary>
    /// <param name="commentaryService">Сервис комментариев</param>
    /// <returns></returns>
    private static async Task<IResult> GetAllCommentaries(ICommentaryService commentaryService)
    {
        try
        {
            return Results.Ok(await commentaryService.GetAllCommentaries());
        }
        catch (Exception exception)
        {
            return Results.BadRequest(exception);
        }
    }

    /// <summary>
    /// Эндпоинт, который возвращает комментарий по его Ай-ди в БД
    /// </summary>
    /// <param name="id"></param>
    /// <param name="commentaryService"></param>
    /// <returns></returns>
    private static async Task<IResult> GetCommentaryById(Guid id, ICommentaryService commentaryService)
    {
        try
        {
            return Results.Ok(await commentaryService.GetCommentaryById(id));
        }
        catch (Exception exception)
        {
            return Results.BadRequest(exception.ToString());
        }
    }

    /// <summary>
    /// Эндпоинт, который возвращает список всех комментариев оставленных конкретным пользователем
    /// </summary>
    /// <param name="authorId"></param>
    /// <param name="commentaryService"></param>
    /// <returns></returns>
    private static async Task<IResult> GetAllUserCommentaries(Guid authorId, ICommentaryService commentaryService)
    {
        try
        {
            return Results.Ok(await commentaryService.GetAllUserCommentaries(authorId));
        }
        catch (Exception exception)
        {
            return Results.BadRequest(exception.ToString());
        }
    }

    /// <summary>
    /// Эндпоинт, который возвращает список всех комментариев оставленных под конкретным постом.
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="commentaryService"></param>
    /// <returns></returns>
    private static async Task<IResult> GetAllPostCommentaries(Guid postId, ICommentaryService commentaryService)
    {
        try
        {
            return Results.Ok(await commentaryService.GetAllPostCommentaries(postId));
        }
        catch (Exception exception)
        {
            return Results.BadRequest(exception.ToString());
        }
    }

    /// <summary>
    /// Эндпоинт, который создаёт комментарий.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="commentaryService"></param>
    /// <returns></returns>
    private static async Task<IResult> CreateCommentary(CommentaryCreateRequest request,
        ICommentaryService commentaryService)
    {
        try
        {
            return Results.Ok(
                await commentaryService.CreateCommentary(request.AuthorId, request.PostId, request.Content));
        }
        catch (Exception exception)
        {
            return Results.BadRequest(exception.ToString());
        }
    }

    /// <summary>
    /// Эндпоинт для удаления комментариев
    /// </summary>
    /// <param name="id"></param>
    /// <param name="commentaryService"></param>
    /// <returns></returns>
    private static async Task<IResult> DeleteCommentary(Guid id, ICommentaryService commentaryService)
    {
        try
        {
            return Results.Ok(await commentaryService.DeleteCommentary(id));
        }
        catch (Exception exception)
        {
            return Results.BadRequest(exception);
        }
    }

    /// <summary>
    /// Эндпоинт для обновления данных комментария
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <param name="commentaryService"></param>
    /// <returns></returns>
    private static async Task<IResult> UpdateCommentary(Guid id, string content, ICommentaryService commentaryService)
    {
        try
        {
            return Results.Ok(await commentaryService.CommentaryUpdate(id, content));
        }
        catch (Exception exception)
        {
            return Results.BadRequest(exception.ToString());
        }
    } 
}