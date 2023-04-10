using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GearHelper_Admin
{
    internal class User
    {
        private int id;
        private String username;
        private String email;
        private String password;
        private bool admin;

        public User(int id, string username, string email, string password)
        {
            this.id = id;
            this.username = username;
            this.email = email;
            this.password = password;
        }


        [JsonProperty("id")]
        public int Id { get => id; set => id = value; }
        [JsonProperty("username")]
        public string Username { get => username; set => username = value; }
        [JsonProperty("email")]
        public string Email { get => email; set => email = value; }
        [JsonProperty("password")]
        public string Password { get => password; set => password = value; }
        [JsonProperty("admin")]
        public bool Admin { get => admin; set => admin = value; }

        public override string ToString()
        {
            String isAdmin = "";
            if (this.admin)
            {
                isAdmin = "yes";
            }
            else
            {
                isAdmin = "no";
            }
            return String.Format("{0,-3} {1,-15} {2,-35} {3,-5}",this.id, this.username, this.email, isAdmin);
        }
    }
}