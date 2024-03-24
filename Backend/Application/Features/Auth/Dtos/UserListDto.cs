namespace Application.Features.Auth.Dtos;

public class UserListDto
{
    public string Username { get; set; }
    public string Avatar { get; set; } = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fletsenhance.io%2F&psig=AOvVaw3s4ZYeZC7YhJmx7KPaT9As&ust=1711310863961000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCPiF__OXi4UDFQAAAAAdAAAAABAE";
    public string Address { get; set; }

    public string ProfileBackground { get; set; } =
        "https://media.springernature.com/lw703/springer-static/image/art%3A10.1038%2F528452a/MediaObjects/41586_2015_Article_BF528452a_Figg_HTML.jpg?as=webp";

    public string Sales { get; set; } = "34eth";
    public List<string> Followers { get; set; } = new List<string>() {"dracula", "vampire", "werewolf"};
    
    
}