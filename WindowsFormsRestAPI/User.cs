namespace WindowsFormsRestAPI
{
    internal class User
    {
        public int Id;
        public string username;
        public string password;
        public string role;

        public User(string usernameName, string password)
        {
            this.username = usernameName;

            this.password = password;
        }

        
    }
}
