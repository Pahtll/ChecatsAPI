namespace Checats.Persistence.Entities;

public class CommentaryEntity
{
    /// <summary>
    /// Ай-ди комментария, используется в качестве ключа в БД
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Контентное наполнение комментария
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Ай-ди автора комментария
    /// </summary>
    public UserEntity Author { get; set; }
    
    /// <summary>
    /// Ай-ди поста, к которому относится комментарий
    /// </summary>
    public PostEntity Post { get; set; }
}