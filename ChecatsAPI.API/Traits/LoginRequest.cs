using System.ComponentModel.DataAnnotations;

namespace ChecatsAPI.Traits;

internal record LoginRequest(
    [Required] string Username,
    [Required] string Password);