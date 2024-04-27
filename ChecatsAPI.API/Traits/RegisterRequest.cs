using System.ComponentModel.DataAnnotations;

namespace ChecatsAPI.Traits;

public record RegisterRequest(
    [Required] string Username,
    [Required] string Email,
    [Required] string Password);