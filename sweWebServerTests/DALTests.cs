using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data.SqlClient;
using sweWebServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace sweWebServer.Tests
{
    [TestClass()]
    public class DALTests
    {
        public string xmlRechnung = @"<Search>
<Rechnung>
<Name>Muris</Name>
</Rechnung>
</Search>";
        public string xmlPerson = @"<Search>
<Person>
<Vorname>Muris</Vorname>
</Person>
</Search>";
        public string xmlFirma = @"<Search>
<Firma>
<UID>090400</UID>
</Firma>
</Search>";
        public string xmlDeletePerson = @"<Delete>
<Person>
<ID>1</ID>
</Person>
</Delete>";
        public string xmlDeleteFirma = @"<Delete>
<Firma>
<ID>090400</ID>
</Firma>
</Delete>";
        public string xmlFirmen = @"<Search>
  <Firmen />
</Search>";
        public string xmlPersonen = @"<Search>
  <Personen />
</Search>";
        public string xmlRechnungszeilen = @"<Search>
<Rechnungszeilen>
<ID>2</ID>
</Rechnungszeilen>
</Search>";
        public string xmlSearchFailing = @"<Search>
  <Failed />
</Search>";

        public string xmlFailing = @"<Fail>
  <Failed />
</Fail>";

        public string select(int type)
        {            
            XElement output = null;
            MockDAL mock = new MockDAL();

            switch (type)
            {
                case -1:
                    output = new XElement("Fail");
                    break;
                case 0:
                    output = new XElement("Personen");
                    break;
                case 1:
                    output = new XElement("Firmen");
                    break;
                case 2:
                    output = new XElement("Rechnungen");
                    break;
                case 3:
                    output = new XElement("Firmen");
                    break;
                case 4:
                    output = new XElement("Rechnungszeilen");
                    break;
                case 5:
                    output = new XElement("Rechnungszeilen");
                    break;
                default:
                    break;
            }
            try
            {               
                    switch (type)
                    {
                        case 0:
                            return mock.dummyPerson("Test");
                            break;
                        case 1:
                            return mock.dummyFirma("Test");
                            break;
                        case 2:
                            return mock.dummyPerson("Test");
                            break;
                        case 3:
                            return mock.dummyFirma("Test");
                            break;
                        case 4:
                            return mock.dummyPerson("Test");
                            break;
                        case 5:
                            return mock.dummyPerson("Test");
                            break;
                        default:
                            break;
                    }
                }            
            finally
            {
                
            }
            return output.ToString();
        }

        [TestMethod()]
        public void DALTest()
        {
            DAL dal = new DAL();

            Assert.IsNotNull(dal);
        }

        [TestMethod()]
        public void openConnectionTest()
        {
            DAL dal = new DAL();
            Assert.IsTrue(dal.openConnection());
        }

        [TestMethod()]
        public void testDummyFirma()
        {
            DAL dal = new DAL();
            Assert.IsNotNull(dal.dummyFirma("TEST"));
        }

        [TestMethod()]
        public void testDummyPerson()
        {
            DAL dal = new DAL();
            Assert.IsNotNull(dal.dummyPerson("TEST"));
        }

        [TestMethod()]
        public void testDummyRechnung()
        {
            DAL dal = new DAL();
            Assert.IsNotNull(dal.dummyRechnung("TEST"));
        }

        [TestMethod()]
        public void updateTest()
        {
            SqlCommand cmd = new SqlCommand();
            DAL dal = new DAL();
            Assert.IsFalse(dal.update(cmd));
        }

        [TestMethod()]
        public void selectPersonTest()
        {
            SqlCommand cmd = new SqlCommand();

            sweWebServer.Tests.BLTests BL = new BLTests();

            BL.DoRequest(xmlPerson);
            Assert.AreEqual(BL.searchType, 0);
        }

        [TestMethod()]
        public void selectFirmaTest()
        {
            SqlCommand cmd = new SqlCommand();

            sweWebServer.Tests.BLTests BL = new BLTests();

            BL.DoRequest(xmlFirma);
            Assert.AreEqual(BL.searchType, 1);
        }

        [TestMethod()]
        public void selectPersonTestFail()
        {
            SqlCommand cmd = new SqlCommand();

            sweWebServer.Tests.BLTests BL = new BLTests();

            BL.DoRequest(xmlFirma);
            Assert.AreNotEqual(BL.searchType, 0);
        }

        [TestMethod()]
        public void selectFirmaTestFail()
        {
            SqlCommand cmd = new SqlCommand();

            sweWebServer.Tests.BLTests BL = new BLTests();

            BL.DoRequest(xmlPerson);
            Assert.AreNotEqual(BL.searchType, 1);
        }

        [TestMethod()]
        public void checkSelectPersonen()
        {
            SqlCommand cmd = new SqlCommand();

            sweWebServer.Tests.BLTests BL = new BLTests();

            BL.DoRequest(xmlPerson);
            XmlDocument xml = new XmlDocument();            
            xml.LoadXml(select(BL.searchType));

            XmlElement root = xml.DocumentElement;

            Assert.AreEqual(root.Name, "Personen");
        }

        [TestMethod()]
        public void checkSelectPerson()
        {
            SqlCommand cmd = new SqlCommand();

            sweWebServer.Tests.BLTests BL = new BLTests();

            BL.DoRequest(xmlPerson);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(select(BL.searchType));

            XmlElement root = xml.DocumentElement;

            Assert.AreEqual(root.FirstChild.Name, "Person");
        }

        [TestMethod()]
        public void checkSelectPersonItemsCountPlus()
        {
            SqlCommand cmd = new SqlCommand();

            sweWebServer.Tests.BLTests BL = new BLTests();

            BL.DoRequest(xmlPerson);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(select(BL.searchType));

            XmlElement root = xml.DocumentElement;

            Assert.AreEqual(root.FirstChild.ChildNodes.Count, 2);
        }

        [TestMethod()]
        public void checkSelectPersonItemsCountAroundMinus()
        {
            SqlCommand cmd = new SqlCommand();

            sweWebServer.Tests.BLTests BL = new BLTests();

            BL.DoRequest(xmlPerson);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(select(BL.searchType));

            XmlElement root = xml.DocumentElement;

            Assert.AreNotEqual(root.FirstChild.ChildNodes.Count, 1);
        }

        [TestMethod()]
        public void checkSelectPersonItemsCountAroundPlus()
        {
            SqlCommand cmd = new SqlCommand();

            sweWebServer.Tests.BLTests BL = new BLTests();

            BL.DoRequest(xmlPerson);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(select(BL.searchType));

            XmlElement root = xml.DocumentElement;

            Assert.AreNotEqual(root.FirstChild.ChildNodes.Count, 3);
        }

        [TestMethod()]
        public void checkSelectFirmen()
        {
            SqlCommand cmd = new SqlCommand();

            sweWebServer.Tests.BLTests BL = new BLTests();

            BL.DoRequest(xmlFirma);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(select(BL.searchType));

            XmlElement root = xml.DocumentElement;

            Assert.AreEqual(root.Name, "Firmen");
        }

        [TestMethod()]
        public void checkSelectFirmenChild()
        {
            SqlCommand cmd = new SqlCommand();

            sweWebServer.Tests.BLTests BL = new BLTests();

            BL.DoRequest(xmlFirma);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(select(BL.searchType));

            XmlElement root = xml.DocumentElement;

            Assert.AreEqual(root.FirstChild.Name, "Firma");
        }

        [TestMethod()]
        public void checkSelectAllFirmen()
        {
            SqlCommand cmd = new SqlCommand();

            sweWebServer.Tests.BLTests BL = new BLTests();

            BL.DoRequest(xmlFirmen);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(select(BL.searchType));

            XmlElement root = xml.DocumentElement;

            Assert.AreNotEqual(root.ChildNodes.Count, 0);
        }

        [TestMethod()]
        public void checkSelectFailWhenDelete()
        {
            SqlCommand cmd = new SqlCommand();

            sweWebServer.Tests.BLTests BL = new BLTests();

            BL.DoRequest(xmlDeleteFirma);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(select(BL.searchType));

            XmlElement root = xml.DocumentElement;

            Assert.AreEqual(root.Name, "Fail");
        }

        [TestMethod()]
        public void checkSelectNotFirmenWhenDelete()
        {
            SqlCommand cmd = new SqlCommand();

            sweWebServer.Tests.BLTests BL = new BLTests();

            BL.DoRequest(xmlDeleteFirma);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(select(BL.searchType));

            XmlElement root = xml.DocumentElement;

            Assert.AreNotEqual(root.Name, "Firmen");
        }
    }
}
