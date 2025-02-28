namespace APIshka.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public User()
        {
            
        }

        public User(string username,
            string passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;
        }

    }
}
