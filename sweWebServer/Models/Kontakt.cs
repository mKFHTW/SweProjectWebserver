using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sweWebServer.Models
{
    class Kontakt
    {
        public int ID { get; set; }
        public string Adresse { get; set; }
        public int PLZ { get; set; }
        public string Ort { get; set; }
    }
}
