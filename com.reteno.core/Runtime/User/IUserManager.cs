namespace Reteno.Users
{
    public interface IUserManager
    {
        string UserId { get; }
        /// <summary>
        /// Sets the user attributes using the specified user
        /// </summary>
        /// <param name="user">The user</param>
        void SetUserAttributes(string externalUserId, User user);
        /// <summary>
        /// Sets the anonymous user attributes using the specified user
        /// </summary>
        /// <param name="user">The user</param>
        void SetAnonymousUserAttributes(UserAttributesAnonymous userAttributesAnonymous);
    }
}