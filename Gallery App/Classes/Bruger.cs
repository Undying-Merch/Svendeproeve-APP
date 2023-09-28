using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery_App.Classes
{
    public class Bruger
    {
        public int id {  get; set; }
        public string navn { get; set; }
        public string brugernavn { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public Bruger() { }
        public Bruger (string brugernavn, string password)
        {
            this.brugernavn = brugernavn;
            this.password = password;
        }
        public Bruger(string navn, string brugernavn, string password, string email)
        {
            this.navn = navn;
            this.brugernavn = brugernavn;
            this.password = password;
            this.email = email;
        }
        public Bruger(int id, string navn, string brugernavn, string password, string email)
        {
            this.id = id;
            this.navn = navn;
            this.brugernavn = brugernavn;
            this.password = password;
            this.email = email;
        }
    }
}
