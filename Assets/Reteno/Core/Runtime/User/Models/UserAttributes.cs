using System.Collections.Generic;

namespace Reteno.User
{
    /// <summary>
    /// The user attributes class
    /// </summary>
    public class UserAttributes
    {
        /// <summary>
        /// Gets or sets the value of the phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the value of the email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the value of the first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the value of the last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the value of the language code
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the value of the time zone
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the value of the address
        /// </summary>
        public UserAddress Address { get; set; }

        /// <summary>
        /// Gets or sets the value of the fields
        /// </summary>
        public List<Field> Fields { get; set; }
    }
}