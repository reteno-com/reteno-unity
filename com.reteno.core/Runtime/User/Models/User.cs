using System.Collections.Generic;

namespace Reteno.Users
{
    /// <summary>
    /// The user class
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the value of the user attributes
        /// </summary>
        public UserAttributes UserAttributes { get; set; }

        /// <summary>
        /// Gets or sets the value of the subscription keys
        /// </summary>
        public List<string> SubscriptionKeys { get; set; }

        /// <summary>
        /// Gets or sets the value of the group names include
        /// </summary>
        public List<string> GroupNamesInclude { get; set; }

        /// <summary>
        /// Gets or sets the value of the group names exclude
        /// </summary>
        public List<string> GroupNamesExclude { get; set; }
    }
}
