namespace Application.Features.UserProfiles.Dtos
{
    public class UpdateProfileDto
    {
        public string Avatar { get; set; }
        public string UserName { get; set; }
        public string Bio { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? YouTube { get; set; }
        public string? Telegram { get; set; }
    }
}