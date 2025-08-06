using System.Collections.Generic;

namespace Task_Tracker.Logic
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; } 
    }

    public class UserService
    {
        private readonly List<User> users = new List<User>();

        public bool Register(string username, string password)
        {
            if (users.Exists(u => u.Username == username))
                return false; // Already exists

            users.Add(new User { Username = username, Password = password });
            return true;
        }

        public bool Login(string username, string password)
        {
            return users.Exists(u => u.Username == username && u.Password == password);
        }
    }
}
