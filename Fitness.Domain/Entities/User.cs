using System.Text.Json.Serialization;
using Fitness.Domain.Abstractions;
using Fitness.Domain.Enums;

namespace Fitness.Domain.Entites;

public class User : Entity
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    [JsonIgnore]
    public string Password { get; set; } = null!;
}