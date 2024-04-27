using System.ComponentModel.DataAnnotations;

namespace ChecatsAPI.Traits;

internal record CommentaryCreateRequest(
    [Required] Guid AuthorId,
    [Required] Guid PostId,
    [Required] string Content);