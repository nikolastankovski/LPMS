namespace LPMS.Domain.Models.CustomModels;

public class LoggedInUser
{
    public Guid AccountId { get; set; }
    public Guid SystemUserId { get; set; }
    public string Name { get; set; } = null!;
    public string Role { get; set; } = null!;
}
