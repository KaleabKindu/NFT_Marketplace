using System;

namespace Application.Features.Auth.Dtos
{
    public class UserFetchDto
    {
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
    }
}