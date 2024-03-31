namespace Application.Contracts
{
    public interface IUserAccessor
    {
        public string GetPublicAddress();
        public string GetUserId();
    }
}
