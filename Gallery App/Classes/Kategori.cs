using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery_App.Classes
{
    internal class Kategori
    {
        public int Id { get; set; }
        public string navn { get; set; }

        public Kategori() { }
        public Kategori(int id, string navn)
        {
            Id = id;
            this.navn = navn;
        }
    }
}
