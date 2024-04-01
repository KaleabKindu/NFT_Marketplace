namespace Application.Features.Auth.Dtos;

public class UserListDto
{
    public string Username { get; set; }
    public string Background { get; set; } = "https://placehold.co/1080x720/png";
    public string Avatar { get; set; } = "https://placehold.co/150x150/png";
    public string Address { get; set; }
    public string Sales { get; set; } = "34eth";
    public Boolean Following { get; set; } = true;



}