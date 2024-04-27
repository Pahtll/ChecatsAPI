using System.ComponentModel.DataAnnotations;

namespace ChecatsAPI.Traits;

public record UpdatePostRequest(
    [Required] Guid Id,
    [Required] string Title,
    [Required] string Content);