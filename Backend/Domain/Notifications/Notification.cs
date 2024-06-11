#nullable enable

namespace Domain.Notifications;

public class Notification : BaseClass
{
    public required AppUser To { get; set; }
    public required string ToId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public bool IsRead { get; set; } = false;
}