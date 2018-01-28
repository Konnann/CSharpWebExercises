namespace MyServer.SendEmailApplication.Handlers
{
    using System.IO;

    public class LoginHandler
    {
        private readonly string databasePath = @"SendEmailApplication\Data\database.csv";

        public bool ValidateLogin(string username, string password)
        {
            var usernames = File.ReadAllLines(databasePath);
            var loginData = $"{username},{password}";

            foreach (var userData in usernames)
            {
                if (userData.Equals(loginData))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
