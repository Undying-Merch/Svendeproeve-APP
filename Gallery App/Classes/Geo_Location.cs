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
        public decimal lattitude { get; set; }
        public decimal longtitude { get; set; }

        public Geo_Location() { }
        public Geo_Location(decimal lattitude, decimal longtitude)
        {
            this.lattitude = lattitude;
            this.longtitude = longtitude;
        }
        public Geo_Location(int id, decimal lattitude, decimal longtitude)
        {
            this.id = id;
            this.lattitude = lattitude;
            this.longtitude = longtitude;
        }
    }
}
