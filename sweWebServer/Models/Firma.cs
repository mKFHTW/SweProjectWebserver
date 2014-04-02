using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sweWebServer.Models
{
    class Firma : Kontakt
    {
        public string UID { get; set; }
        public string Name { get; set; }
    }
}
