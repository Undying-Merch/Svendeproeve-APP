using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery_App.Classes
{
    internal class Billede
    {
        public int id {  get; set; }
        public string titel { get; set; }
        public string beskrivelse { get; set; }
        public FileResult billedet { get; set; }
        public int geo_id { get; set; }
        public int kategori_id { get; set; }
        public int upload_id {  get; set; }

        public Billede() { }
        public Billede(string titel, string beskrivelse, FileResult billedet, int geo_id, int kategori_id, int upload_id)
        {
            this.titel = titel;
            this.beskrivelse = beskrivelse;
            this.billedet = billedet;
            this.geo_id = geo_id;
            this.kategori_id = kategori_id;
            this.upload_id = upload_id;
        }
        public Billede(int id, string titel, string beskrivelse, FileResult billedet, int geo_id, int kategori_id, int upload_id)
        {
            this.id = id;
            this.titel = titel;
            this.beskrivelse = beskrivelse;
            this.billedet = billedet;
            this.geo_id = geo_id;
            this.kategori_id = kategori_id;
            this.upload_id = upload_id;
        }
    }
}
