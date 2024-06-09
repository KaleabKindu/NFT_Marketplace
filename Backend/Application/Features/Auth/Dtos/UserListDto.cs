﻿namespace Application.Features.Auth.Dtos;

public class UserListDto
{
    public string Username { get; set; }
    public string Background { get; set; }
    public string Avatar { get; set; }
    public string Address { get; set; }
    public string Sales { get; set; }
    public Boolean Following { get; set; } = true;
}