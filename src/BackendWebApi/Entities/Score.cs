namespace BackendWebApi.Entities;

public class Score
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Course { get; set; } = string.Empty;
    public int Value { get; set; }
    public User User { get; set; } = null!;
}