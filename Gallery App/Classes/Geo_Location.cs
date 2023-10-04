using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery_App.Classes
{
    internal class Geo_Location
    {
        public int id {  get; set; }
        public string lattitude { get; set; }
        public string longtitude { get; set; }

        public Geo_Location() { }
        public Geo_Location(string lattitude, string longtitude)
        {
            this.lattitude = lattitude;
            this.longtitude = longtitude;
        }
        public Geo_Location(int id, string lattitude, string longtitude)
        {
            this.id = id;
            this.lattitude = lattitude;
            this.longtitude = longtitude;
        }
    }
}
