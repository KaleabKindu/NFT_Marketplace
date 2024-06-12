using System;
using Application.Features.Common;

namespace Application.Features.Auth.Dtos
{
    public class UserFetchDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
    }
}