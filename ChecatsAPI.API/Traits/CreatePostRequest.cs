using System.ComponentModel.DataAnnotations;

namespace ChecatsAPI.Traits;

public record CreatePostRequest(
    [Required] string Title,
    [Required] string Content,
    [Required] Guid AuthorId);