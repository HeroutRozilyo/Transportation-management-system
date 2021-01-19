namespace BO
{
    /// <summary>
    /// An entity that holds the software user for me
    /// </summary>
    public class User
    {
        /// <summary>
        /// Username (Unique ID)
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password (for personal login)
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// If the user is an administrator or a regular user
        /// </summary>
        public bool Admin { get; set; }


        /// <summary>
        /// An existing user in the system?
        /// </summary>
        public bool UserExist { get; set; }

        /// <summary>
        /// Password recovery email address
        /// </summary>
        public string MailAddress { get; set; }
    }
}
