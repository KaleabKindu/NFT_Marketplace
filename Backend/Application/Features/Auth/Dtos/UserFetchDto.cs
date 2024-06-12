using System;
using Application.Features.Common;

namespace Application.Features.Auth.Dtos
{
    public class UserFetchDto : BaseDto
    {
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
    }
}