namespace RetenoSDK.User
{
    public interface IUserManager
    {
        string UserId { get; }
        void AddUserId(string userId);
    }
}