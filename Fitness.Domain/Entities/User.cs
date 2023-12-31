using System.Text.Json.Serialization;
using Fitness.Domain.Abstractions;
using Fitness.Domain.Enums;

namespace Fitness.Domain.Entites;

public class User : Entity
{
    public String Username { get; set; } = null!;
    public String Email { get; set; } = null!;
    public Role Role { get; set; }
    [JsonIgnore]
    public String Password { get; set; } = null!;
}