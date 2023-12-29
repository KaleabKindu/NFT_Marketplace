using Domain;

namespace Application.Contracts.Services
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user, int expireInDays);
    }
}