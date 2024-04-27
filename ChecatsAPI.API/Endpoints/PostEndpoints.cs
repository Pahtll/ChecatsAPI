using Checats.Application.Interfaces;
using ChecatsAPI.Traits;

namespace ChecatsAPI.Endpoints;

public static class PostEndpoints
{
    public static IEndpointRouteBuilder MapPostEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("GetAllPosts", GetAllPosts);
        app.MapGet("GetPostById", GetPostById);
        app.MapGet("GetPostByTitle", GetPostByTitle);
        app.MapPost("CreatePost", CreatePost);
        app.MapPut("UpdatePost", UpdatePost);
        app.MapDelete("DeletePost", DeletePost);

        return app;
    }

    /// <summary>
    /// Эндпоинт, который возвращает список всех постов
    /// </summary>
    /// <param name="postService"></param>
    /// <returns></returns>
    private static async Task<IResult> GetAllPosts(IPostService postService)
    {
        try
        {
            return Results.Ok(await postService.GetAllPosts());
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.ToString());
        }
    }

    /// <summary>
    /// Эндпоинт, который возвращает пост по конкретному Ай-ди
    /// </summary>
    /// <param name="id"></param>
    /// <param name="postService"></param>
    /// <returns></returns>
    private static async Task<IResult> GetPostById(Guid id, IPostService postService)
    {
        try
        {
            return Results.Ok(await postService.GetPostById(id));
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.ToString());
        }
    }

    /// <summary>
    /// Эндпоинт, который возвращает пост по его названию
    /// </summary>
    /// <param name="title"></param>
    /// <param name="postService"></param>
    /// <returns></returns>
    private static async Task<IResult> GetPostByTitle(string title, IPostService postService)
    {
        try
        {
            return Results.Ok(await postService.GetPostByTitle(title));
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.ToString());
        }
    }

    /// <summary>
    /// Эндпоинт, который отвечает за создание постов
    /// </summary>
    /// <param name="createPostRequest"></param>
    /// <param name="postService"></param>
    /// <returns></returns>
    private static async Task<IResult> CreatePost(CreatePostRequest createPostRequest, IPostService postService)
    {
        try
        {
            return Results.Ok(await postService.CreatePost(createPostRequest.AuthorId, createPostRequest.Title, createPostRequest.Content));
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.ToString());
        }
    }

    /// <summary>
    /// Эндпоинт для удаления постов
    /// </summary>
    /// <param name="id"></param>
    /// <param name="postService"></param>
    /// <returns></returns>
    private static async Task<IResult> DeletePost(Guid id, IPostService postService)
    {
        try
        {
            return Results.Ok(await postService.DeletePost(id));
        }
        catch (Exception exception)
        {
            return Results.BadRequest(exception.ToString());
        }
    }

    /// <summary>
    /// Эндпоинт, который отвечает за обновление поста
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatePostRequest"></param>
    /// <param name="postService"></param>
    /// <returns></returns>
    private static async Task<IResult> UpdatePost(Guid id, UpdatePostRequest updatePostRequest, IPostService postService)
    {
        try
        {
            return Results.Ok(await postService.UpdatePost(id, updatePostRequest.Title, updatePostRequest.Content));
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.ToString());
        }
    }
}