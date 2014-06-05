using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace sweWebServer
{
    public class MockDAL
    {
        public string dummyFirma(string data)
        {
            //xml.LoadXml(data);

            Models.Firma obj = new Models.Firma();            
            obj.Name = "Microsoft";
            obj.UID = "ATU1278";

            XElement output = new XElement("Firmen");
            XElement firma = new XElement("Firma",
                    new XElement("Name", obj.Name),
                    new XElement("UID", obj.UID)
                    );
            output.Add(firma);
            return output.ToString();
        }

        public string dummyPerson(string data)
        {
            //xml.LoadXml(data);

            Models.Person obj = new Models.Person();
            obj.Vorname = "Muris";
            obj.Nachname = "Kavlak";

            XElement output = new XElement("Personen");
            XElement person = new XElement("Person",
                    new XElement("Vorname", obj.Vorname),
                    new XElement("Nachname", obj.Nachname)
                    );
            output.Add(person);
            return output.ToString();
        }

        public string dummyRechnung(string data)
        {
            //xml.LoadXml(data);

            Models.Firma obj = new Models.Firma();
            obj.Name = "Microsoft";
            obj.UID = "ATU1278";

            XElement output = new XElement("Firmen");
            XElement firma = new XElement("Firma",
                    new XElement("Name", obj.Name),
                    new XElement("UID", obj.UID)
                    );
            output.Add(firma);
            return output.ToString();
        }
    }
}
