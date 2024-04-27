using Checats.Persistence.Entities;

namespace Checats.Application.Interfaces
{
    public interface ICommentaryService
    {
        Task<Guid> CommentaryUpdate(Guid id, string content);
        Task<Guid> CreateCommentary(Guid authorId, Guid postId, string content);
        Task<Guid> DeleteCommentary(Guid id);
        Task<List<CommentaryEntity>> GetAllCommentaries();
        Task<List<CommentaryEntity>?> GetAllPostCommentaries(Guid postId);
        Task<List<CommentaryEntity>?> GetAllUserCommentaries(Guid authorId);
        Task<CommentaryEntity> GetCommentaryById(Guid id);
    }
}