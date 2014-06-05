using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sweWebServer.Models
{
    class Kontakt
    {
        public string ID { get; set; }
        public string Adresse { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string RechnungsAdresse { get; set; }
        public string RechnungsPLZ { get; set; }
        public string RechnungsOrt { get; set; }
        public string LieferAdresse { get; set; }
        public string LieferPLZ { get; set; }
        public string LieferOrt { get; set; }
    }
}
