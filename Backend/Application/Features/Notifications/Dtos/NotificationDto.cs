namespace Application.Features.Notifications.Dtos;

public class NotificationDto
{
    public long Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public bool IsRead { get; set; }
    public DateTime Date { get; set; }
}


