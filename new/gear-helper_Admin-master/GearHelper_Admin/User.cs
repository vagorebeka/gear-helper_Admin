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
        private String name;
        private String email;
        private String password;
        private bool admin;

        public User(int id, String name, String email, String password, bool admin)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.password = password;
            this.admin = admin;
        }


        [JsonProperty("id")]
        public int Id { get => id; set => id = value; }
        [JsonProperty("name")]
        public String Name { get => name; set => name = value; }
        [JsonProperty("email")]
        public String Email { get => email; set => email = value; }
        [JsonProperty("password")]
        public String Password { get => password; set => password = value; }
        [JsonProperty("admin")]
        public bool Admin { get => admin; set => admin = value; }

        public override String ToString()
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
            return String.Format("{0,-3} {1,-15} {2,-35} {3,-5}",this.id, this.name, this.email, isAdmin);
        }
    }
}