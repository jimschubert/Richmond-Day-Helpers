using System.Configuration;

namespace RichmondDay.Helpers{
    public class SmtpConfiguration {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DefaultFromAddress { get; set; }

        public SmtpConfiguration(string server, string port, string username, string password, string defaultFromAddress) {
            Server = server;
            Port = int.Parse(port);
            Username = username;
            Password = password;
            DefaultFromAddress = defaultFromAddress;
        }
    }
}
