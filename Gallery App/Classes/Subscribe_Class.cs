using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery_App.Classes
{
    public class Subscribe_Class
    {
        public int id {  get; set; }
        public int bruger_id { get; set; }
        public int kategori_id {  get; set; }

        public Subscribe_Class() { }
        public Subscribe_Class(int bruger_id, int kategori_id)
        {
            this.bruger_id = bruger_id;
            this.kategori_id = kategori_id;
        }
        public Subscribe_Class(int id, int bruger_id, int kategori_id)
        {
            this.id = id;
            this.bruger_id= bruger_id;
            this.kategori_id= kategori_id;
        }
    }
}
