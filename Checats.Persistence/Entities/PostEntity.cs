namespace Checats.Persistence.Entities;

public class PostEntity
{
    /// <summary>
    /// Ай-ди поста
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название поста
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Контент содержание поста
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Ай-ди автора поста
    /// </summary>
    public UserEntity Author { get; set; }
    
    /// <summary>
    /// Список Ай-ди Всех комментариев к посту
    /// </summary>
    public List<CommentaryEntity>? Commentaries { get; set; }
}