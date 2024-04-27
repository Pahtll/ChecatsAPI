using Checats.Persistence.Entities;
using ChecatsAPI.Domain.Models;

namespace Checats.Application.Interfaces
{
    public interface IPostService
    {
        Task<Guid> CreatePost(Guid authorId, string title, string content);
        Task<Guid> DeletePost(Guid id);
        Task<List<PostEntity>> GetAllPosts();
        Task<List<PostEntity>> GetAllUserPosts(Guid authorId);
        Task<PostEntity> GetPostById(Guid id);
        Task<PostEntity> GetPostByTitle(string title);
        Task<Guid> UpdatePost(Guid id, string title, string content);
    }
}