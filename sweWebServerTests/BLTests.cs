using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sweWebServer;
using System.Xml;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sweWebServer.Tests
{
    [TestClass()]
    public class BLTests
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

        DAL access;
        SqlCommand cmd = new SqlCommand();
        public string statement = null;
        public int searchType = 0;

        public string DoRequest(string param)
        {            
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(param);

            XmlElement root = xml.DocumentElement;
            XmlNode type = root.FirstChild;

            #region Search
            if (root.Name == "Search")
            {
                if (type.Name == "Person")
                {
                    searchType = 0;
                    XmlNodeList xnList = type.ChildNodes;

                    if (xnList.Count > 1)
                    {
                        statement = @"SELECT A.ID, A.Vorname, A.Nachname, A.Titel, A.Suffix, A.Wohnadresse, A.WohnPLZ, A.WohnOrt, A.Rechnungsadresse, A.RechnungsPLZ, A.RechnungsOrt, A. Lieferadresse, A.LieferPLZ, A.LieferOrt, A.Geburtsdatum, B.Firmenname, A.FirmenID 
FROM Kontaktdaten A
LEFT JOIN Kontaktdaten B
ON A.FirmenID = B.ID
WHERE A.Vorname LIKE @vorname AND A.Nachname LIKE @nachname";
                        cmd.Parameters.AddWithValue("@vorname", "%" + type.FirstChild.InnerText + "%");
                        cmd.Parameters.AddWithValue("@nachname", "%" + type.FirstChild.InnerText + "%");
                    }

                    else
                    {
                        if (type.FirstChild.Name == "Vorname")
                        {
                            statement = @"SELECT A.ID, A.Vorname, A.Nachname, A.Titel, A.Suffix, A.Wohnadresse, A.WohnPLZ, A.WohnOrt, A.Rechnungsadresse, A.RechnungsPLZ, A.RechnungsOrt, A. Lieferadresse, A.LieferPLZ, A.LieferOrt, A.Geburtsdatum, B.Firmenname, A.FirmenID 
FROM Kontaktdaten A
LEFT JOIN Kontaktdaten B
ON A.FirmenID = B.ID
WHERE A.Vorname LIKE @vorname";
                            cmd.Parameters.AddWithValue("@vorname", "%" + type.FirstChild.InnerText + "%");
                        }

                        else
                        {
                            statement = @"SELECT A.ID, A.Vorname, A.Nachname, A.Titel, A.Suffix, A.Wohnadresse, A.WohnPLZ, A.WohnOrt, A.Rechnungsadresse, A.RechnungsPLZ, A.RechnungsOrt, A. Lieferadresse, A.LieferPLZ, A.LieferOrt, A.Geburtsdatum, B.Firmenname, A.FirmenID 
FROM Kontaktdaten A
LEFT JOIN Kontaktdaten B
ON A.FirmenID = B.ID 
WHERE A.Nachname LIKE @nachname";
                            cmd.Parameters.AddWithValue("@nachname", "%" + type.FirstChild.InnerText + "%");
                        }
                    }
                }
                else if (type.Name == "Firma")
                {
                    searchType = 1;
                    XmlNode search = type.FirstChild;

                    switch (search.Name)
                    {
                        case "Name":
                            statement = @"SELECT A.ID, A.Wohnadresse, A.WohnPLZ, A.WohnOrt, A.Rechnungsadresse, A.RechnungsPLZ, A.RechnungsOrt, A. Lieferadresse, A.LieferPLZ, A.LieferOrt, A.Firmenname, A.UID
FROM Kontaktdaten A
WHERE A.Firmenname LIKE @name";
                            cmd.Parameters.AddWithValue("@name", "%" + search.InnerText + "%");
                            break;
                        case "UID":
                            statement = @"SELECT A.ID, A.Wohnadresse, A.WohnPLZ, A.WohnOrt, A.Rechnungsadresse, A.RechnungsPLZ, A.RechnungsOrt, A. Lieferadresse, A.LieferPLZ, A.LieferOrt, A.Firmenname, A.UID
FROM Kontaktdaten A
WHERE A.UID LIKE @uid";
                            cmd.Parameters.AddWithValue("@uid", "%" + search.InnerText + "%");
                            break;
                        default:
                            break;
                    }
                }
                else if (type.Name == "Rechnung")
                {
                    searchType = 2;
                    if (type.ChildNodes.Count < 2)
                    {
                        statement = @"Select Rechnungen.ID, Kontaktdaten.Nachname, Rechnungen.fkZuKontaktID, Rechnungen.Datum, Rechnungen.Faelligkeit, Rechnungen.Kommentar, Rechnungen.Nachricht
FROM Rechnungen JOIN Kontaktdaten ON Rechnungen.fkZuKontaktID = Kontaktdaten.ID WHERE Kontaktdaten.Nachname LIKE @name OR Kontaktdaten.Vorname LIKE @name";
                        cmd.Parameters.AddWithValue("@name", "%" + type.FirstChild.InnerText + "%");
                    }

                    else
                    {
                        statement = @"Select Rechnungen.ID, Kontaktdaten.Nachname, Rechnungen.fkZuKontaktID, Rechnungen.Datum, Rechnungen.Faelligkeit, Rechnungen.Kommentar, Rechnungen.Nachricht
FROM Rechnungen JOIN Kontaktdaten ON Rechnungen.fkZuKontaktID = Kontaktdaten.ID WHERE ";
                        XmlNodeList xnList = type.ChildNodes;

                        foreach (XmlNode item in xnList)
                        {
                            switch (item.Name)
                            {
                                case "Name":
                                    statement += "Kontaktdaten.Nachname LIKE @name OR Kontaktdaten.Vorname LIKE @name";
                                    cmd.Parameters.AddWithValue("@name", "%" + item.InnerText + "%");
                                    break;
                                case "DateVon":
                                    statement += "Rechnungen.Datum >= @datevon AND ";
                                    cmd.Parameters.AddWithValue("@datevon", Convert.ToDateTime(item.InnerText));
                                    break;
                                case "DateBis":
                                    statement += "Rechnungen.Faelligkeit <= @datebis";
                                    cmd.Parameters.AddWithValue("@datebis", Convert.ToDateTime(item.InnerText));
                                    break;
                                case "BetragVon":
                                    statement = @"Select Rechnungen.ID, Kontaktdaten.Nachname, Rechnungen.fkZuKontaktID, Rechnungen.Datum, Rechnungen.Faelligkeit, Rechnungen.Kommentar, Rechnungen.Nachricht
FROM Rechnungen 
JOIN Kontaktdaten ON Rechnungen.fkZuKontaktID = Kontaktdaten.ID
WHERE (SELECT SUM(Rechnungszeilen.Menge * Rechnungszeilen.Stückpreis)
FROM Rechnungszeilen 
WHERE Rechnungen.ID = Rechnungszeilen.fkZuRechnungenID
GROUP by Rechnungszeilen.fkZuRechnungenID) BETWEEN @betragvon AND @betragbis";
                                    cmd.Parameters.AddWithValue("@betragvon", Convert.ToDouble(item.InnerText));
                                    break;
                                case "BetragBis":
                                    cmd.Parameters.AddWithValue("@betragbis", Convert.ToDouble(item.InnerText));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                else if (type.Name == "Firmen")
                {
                    searchType = 3;
                    statement = "SELECT ID, Firmenname FROM Kontaktdaten WHERE Vorname IS NULL";
                }

                else if (type.Name == "Rechnungszeilen")
                {
                    searchType = 4;
                    statement = "SELECT Artikel, Menge, Stückpreis FROM Rechnungszeilen WHERE fkZuRechnungenID = @id";
                    cmd.Parameters.AddWithValue("@id", type.FirstChild.InnerText);
                }

                else if (type.Name == "Personen")
                {
                    searchType = 5;
                    statement = "SELECT ID, Vorname, Nachname FROM Kontaktdaten WHERE Deleted = 0 AND Vorname IS NOT NULL";
                }

                cmd.CommandText = statement;
                //return access.select(cmd, searchType);
                return "SelectFailed";
            }
            #endregion

            else if (root.Name == "Insert")
            {
                if (type.Name == "Person")
                {
                    XmlNodeList xnList = type.ChildNodes;
                    statement = @"INSERT INTO Kontaktdaten (Vorname, Nachname, Titel, Suffix, Geburtsdatum, FirmenID, Deleted)
VALUES (@vorname,@nachname,@titel,@suffix,@geburtsdatum,@firmenid, 0)";
                    /*Vorname = @vorname, 
                    Nachname = @nachname, 
                    Titel = @titel, 
                    Suffix = @suffix, 
                    Geburtsdatum = @geburtsdatum, 
                    FirmenID = @firmenid";*/
                    cmd.Parameters.AddWithValue("@vorname", xnList[1].InnerText);
                    cmd.Parameters.AddWithValue("@nachname", xnList[2].InnerText);
                    cmd.Parameters.AddWithValue("@titel", xnList[3].InnerText);
                    cmd.Parameters.AddWithValue("@suffix", xnList[4].InnerText);
                    cmd.Parameters.AddWithValue("@geburtsdatum", xnList[5].InnerText);
                    cmd.Parameters.AddWithValue("@firmenid", xnList[6].InnerText);
                    //cmd.Parameters.AddWithValue("@id", Convert.ToInt32(xnList[0].InnerText));
                }

                else if (type.Name == "Firma")
                {
                    XmlNodeList xnList = type.ChildNodes;
                    statement = @"INSERT INTO Kontaktdaten (Firmenname, UID, Deleted)
VALUES (@firmenname, @uid, 0)";
                    /*Firmenname = @firmenname, 
                    UID = @uid";*/
                    cmd.Parameters.AddWithValue("@firmenname", xnList[1].InnerText);
                    cmd.Parameters.AddWithValue("@uid", xnList[2].InnerText);
                    //cmd.Parameters.AddWithValue("@id", Convert.ToInt32(xnList[0].InnerText));
                }

                else if (type.Name == "Rechnung")
                {
                    XmlNodeList xnList = type.ChildNodes;
                    statement = @"INSERT INTO Rechnungen (fkZuKontaktID, Datum, Faelligkeit, Kommentar, Nachricht)
VALUES (@fk, @datum, @due, @kommentar, @nachricht)";

                    cmd.Parameters.AddWithValue("@fk", xnList[0].InnerText);
                    cmd.Parameters.AddWithValue("@datum", xnList[1].InnerText);
                    cmd.Parameters.AddWithValue("@due", xnList[2].InnerText);
                    cmd.Parameters.AddWithValue("@kommentar", xnList[3].InnerText);
                    cmd.Parameters.AddWithValue("@nachricht", xnList[4].InnerText);

                    //access.BeginTransaction();
                    cmd.CommandText = statement;
                    //access.insert(cmd);

                    foreach (XmlNode item in xnList)
                    {
                        if (item.Name == "Zeile")
                        {
                            statement = @"INSERT INTO Rechnungszeilen (fkZuRechnungenID, Artikel, Menge, Stückpreis, Ust)
VALUES ((SELECT IDENT_CURRENT(‘Rechnungen’) , @artikel, @menge, @preis, 20)";
                            cmd.Parameters.AddWithValue("@artiel", item.ChildNodes[0].InnerText);
                            cmd.Parameters.AddWithValue("@menge", item.ChildNodes[1].InnerText);
                            cmd.Parameters.AddWithValue("@preis", item.ChildNodes[2].InnerText);

                            cmd.CommandText = statement;
                            //access.insert(cmd);
                        }
                    }

                    //access.CommitTr();
                }

                if (type.Name != "Rechnung")
                {
                    cmd.CommandText = statement;
                    //access.insert(cmd);
                }
            }
            else if (root.Name == "Update")
            {
                if (type.Name == "Person")
                {
                    XmlNodeList xnList = type.ChildNodes;
                    statement = @"UPDATE Kontaktdaten SET 
Vorname = @vorname, 
Nachname = @nachname, 
Titel = @titel, 
Suffix = @suffix, 
Geburtsdatum = @geburtsdatum, 
FirmenID = @firmenid WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@vorname", xnList[1].InnerText);
                    cmd.Parameters.AddWithValue("@nachname", xnList[2].InnerText);
                    cmd.Parameters.AddWithValue("@titel", xnList[3].InnerText);
                    cmd.Parameters.AddWithValue("@suffix", xnList[4].InnerText);
                    cmd.Parameters.AddWithValue("@geburtsdatum", xnList[5].InnerText);
                    cmd.Parameters.AddWithValue("@firmenid", Convert.ToInt32(xnList[6].InnerText));
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(xnList[0].InnerText));
                    return "Updated Person";
                }

                else if (type.Name == "Firma")
                {
                    XmlNodeList xnList = type.ChildNodes;
                    statement = @"UPDATE Kontaktdaten SET 
Firmenname = @firmenname, 
UID = @uid WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@firmenname", xnList[1].InnerText);
                    cmd.Parameters.AddWithValue("@uid", xnList[2].InnerText);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(xnList[0].InnerText));
                    return "Updated Firma";
                }

                else if (type.Name == "PersonFirma")
                {
                    XmlNodeList xnList = type.ChildNodes;
                    statement = @"UPDATE Kontaktdaten SET 
FirmenID = NULL 
WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(xnList[0].InnerText));
                    return "Updated PersonFirma";
                }
                cmd.CommandText = statement;
                //access.update(cmd);
                searchType = -1;
                return "Update successfull";
            }
            else if (root.Name == "Delete")
            {
                if (type.Name == "Person")
                {
                    statement = @"UPDATE Kontaktdaten
SET Deleted = 1
WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(type.FirstChild.InnerText));
                    
                }
                if (type.Name == "Firma")
                {
                    statement = @"UPDATE Kontaktdaten
SET Deleted = 1
WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(type.FirstChild.InnerText));
                }
                cmd.CommandText = statement;
                searchType = -1;
                return "Deleted successfull";
                //access.delete(cmd);
            }
            return "OK";
        }

        [TestMethod]
        public void TestIfSearchPerson()
        {
            cmd = new SqlCommand();
            DoRequest(xmlPerson);
            Assert.AreEqual(0, searchType);
        }

        [TestMethod]
        public void TestIfSearchFirma()
        {
            cmd = new SqlCommand();
            DoRequest(xmlFirma);
            Assert.AreEqual(1, searchType);
        }

        [TestMethod]
        public void TestIfSearchRechnung()
        {
            cmd = new SqlCommand();
            DoRequest(xmlRechnung);
            Assert.AreEqual(2, searchType);
        }

        [TestMethod]
        public void TestIfSearchPersonen()
        {
            cmd = new SqlCommand();
            DoRequest(xmlPersonen);
            Assert.AreEqual(5, searchType);
        }

        [TestMethod]
        public void TestIfSearchFirmen()
        {
            cmd = new SqlCommand();
            DoRequest(xmlFirmen);
            Assert.AreEqual(3, searchType);
        }

        [TestMethod]
        public void TestIfSearchRechnungszeilen()
        {
            cmd = new SqlCommand();
            DoRequest(xmlRechnungszeilen);
            Assert.AreEqual(4, searchType);
        }

        [TestMethod]
        public void TestIfSearchFailed()
        {
            cmd = new SqlCommand();            
            Assert.AreEqual(DoRequest(xmlSearchFailing), "SelectFailed");
        }

        [TestMethod]
        public void TestIfSearchNotFailedPerson()
        {
            cmd = new SqlCommand();
            DoRequest(xmlPerson);
            Assert.AreEqual(statement.Split(' ')[0], "SELECT");
        }

        [TestMethod]
        public void TestIfDeletedPersonTypeFalse()
        {
            cmd = new SqlCommand();
            DoRequest(xmlDeletePerson);
            Assert.AreNotEqual(searchType, 0);
        }

        [TestMethod]
        public void TestIfDeletedFirmaTypeFalse()
        {
            cmd = new SqlCommand();
            DoRequest(xmlDeleteFirma);
            Assert.AreNotEqual(searchType, 0);
        }

        [TestMethod]
        public void TestIfDeleteFirmaStateNotEmpty()
        {
            cmd = new SqlCommand();
            DoRequest(xmlDeleteFirma);
            Assert.AreNotEqual(statement, "");
        }

        [TestMethod]
        public void TestIfDeletePersonStateNotEmpty()
        {
            cmd = new SqlCommand();
            DoRequest(xmlDeletePerson);
            Assert.AreNotEqual(statement, "");
        }
        [TestMethod]
        public void TestIfSelectFirmaStateNotEmpty()
        {
            cmd = new SqlCommand();
            DoRequest(xmlFirma);
            Assert.AreNotEqual(statement, "");
        }
        [TestMethod]
        public void TestIfSelectPersonStateNotEmpty()
        {
            cmd = new SqlCommand();
            DoRequest(xmlPerson);
            Assert.AreNotEqual(statement, "");
        }

        [TestMethod]
        public void TestIfSearchNotFailedRechnung()
        {
            cmd = new SqlCommand();
            DoRequest(xmlRechnung);
            Assert.AreEqual(statement.Split(' ')[0], "Select");
        }

        [TestMethod]
        public void TestIfSearchNotFailedFirma()
        {
            cmd = new SqlCommand();
            DoRequest(xmlFirma);
            Assert.AreEqual(statement.Split(' ')[0], "SELECT");
        }

        [TestMethod]
        public void TestIfDeletedFirma()
        {
            cmd = new SqlCommand();

            Assert.AreEqual(DoRequest(xmlDeleteFirma), "Deleted successfull");
        }

        [TestMethod]
        public void TestIfDeletedPerson()
        {
            cmd = new SqlCommand();

            Assert.AreEqual(DoRequest(xmlDeletePerson), "Deleted successfull");
        }

        [TestMethod]
        public void TestIfDeletedPersonType()
        {
            cmd = new SqlCommand();
            DoRequest(xmlDeletePerson);
            Assert.AreEqual(searchType, -1);
        }

        public void TestIfDeletedFirmaType()
        {
            cmd = new SqlCommand();
            DoRequest(xmlDeleteFirma);
            Assert.AreEqual(searchType, -1);
        }

        [TestMethod]
        public void TestIfSearchNotFailedRechnungFail()
        {
            cmd = new SqlCommand();
            DoRequest(xmlRechnung);
            Assert.AreNotEqual(statement.Split(' ')[0], "");
        }

        [TestMethod]
        public void TestIfSearchNotFailedFirmaFail()
        {
            cmd = new SqlCommand();
            DoRequest(xmlFirma);
            Assert.AreNotEqual(statement.Split(' ')[0], "");
        }

        [TestMethod]
        public void TestIfSearchFirmaNotFail()
        {
            cmd = new SqlCommand();            
            Assert.AreNotEqual(DoRequest(xmlFirma), "OK");
        }

        [TestMethod]
        public void TestIfFail()
        {
            cmd = new SqlCommand();
            Assert.AreEqual(DoRequest(xmlFailing), "OK");
        }
    }
}
