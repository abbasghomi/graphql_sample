namespace BackendWebApi.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DisplayName => $"{Name} ({Email})"; // Calculated field
    public ICollection<Score> Scores { get; set; } = new HashSet<Score>();
}