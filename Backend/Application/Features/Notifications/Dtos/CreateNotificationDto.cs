
namespace Application.Features.Notifications.Dtos;

public class CreateNotificationDto
{
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}