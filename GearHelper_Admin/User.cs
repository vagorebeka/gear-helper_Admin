using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfLogin
{
    public class User
    {
        private String username;
        private String email;
        private String password;

        public User(string username, string email, string password)
        {
            this.username = username;
            this.email = email;
            this.password = password;
        }

        [JsonProperty("username")]
        public string Username { get => username; set => username = value; }
        [JsonProperty("email")]
        public string Email { get => email; set => email = value; }
        [JsonProperty("password")]
        public string Password { get => password; set => password = value; }
    }
}