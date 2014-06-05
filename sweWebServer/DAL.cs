using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;
using System.Configuration;

namespace sweWebServer
{
    public class DAL
    {
        SqlConnection connection;
        XmlDocument xml;
        SqlCommand cmd;
        SqlTransaction tx;
        string statement;       

        public DAL()
        {
            xml = new XmlDocument();

            openConnection();
                
        }
        public void BeginTransaction()
        {
            tx = connection.BeginTransaction();
        }

        public void CommitTr()
        {
            tx.Commit();
        }

        public void RollBackTr()
        {
            tx.Rollback();
        }

        public bool openConnection()
        {
            try
            {
                connection = new SqlConnection(ConfigurationSettings.AppSettings["Server"]);

                connection.Open();
                cmd = new SqlCommand(statement, connection);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string dummyFirma(string data)
        {
            //xml.LoadXml(data);

            Models.Firma obj = new Models.Firma();
            obj.Name = "Microsoft";
            obj.UID = "ATU1278";

            XElement output =
                new XElement("Type", "Firma",
                    new XElement("Name", obj.Name),
                    new XElement("UID", obj.UID)
                    );
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

        public bool insert(SqlCommand command)
        {
            try
            {
                command.Connection = connection;
                command.Transaction = tx;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool update(SqlCommand command)
        {
            try
            {
                command.Connection = connection;
                command.Transaction = tx;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }

        public bool delete(SqlCommand command)
        {
            try
            {
                command.Connection = connection;
                command.Transaction = tx;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }     
        }

        public string select(SqlCommand command, int type)
        {
            command.Connection = connection;
            command.Transaction = tx;
            SqlDataReader reader = command.ExecuteReader();
            XElement output = null;

            switch (type)
            {
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
                while (reader.Read())
                {
                    switch (type)
                    {
                        case 0:                            
                            XElement person = new XElement("Person");
                            person.Add(new XElement("ID", reader[0]));
                            person.Add(new XElement("Vorname", reader[1]));
                            person.Add(new XElement("Nachname", reader[2]));
                            if (reader[3] != "NULL")                            
                                person.Add(new XElement("Titel", reader[3]));                            
                                person.Add(new XElement("Suffix", reader[4]));                            
                                person.Add(new XElement("Adresse", reader[5]));
                                person.Add(new XElement("PLZ", reader[6]));
                                person.Add(new XElement("Ort", reader[7]));
                                if (reader[8] != "NULL") 
                                person.Add(new XElement("RechnungsAdresse", reader[8]));
                                if (reader[9] != "NULL") 
                                person.Add(new XElement("RechnungsPLZ", reader[9]));
                                if (reader[10] != "NULL") 
                                person.Add(new XElement("RechnungsOrt", reader[10]));
                                if (reader[11] != "NULL") 
                                person.Add(new XElement("LieferAdresse", reader[11]));
                                if (reader[12] != "NULL") 
                                person.Add(new XElement("LieferPLZ", reader[12]));
                                if (reader[13] != "NULL") 
                                person.Add(new XElement("LieferOrt", reader[13]));
                                if (reader[14] != "NULL")
                                    person.Add(new XElement("Geburtsdatum", Convert.ToDateTime(reader[14]).ToString("dd/MM/yyyy")));
                                if (reader[15] != "NULL") 
                                person.Add(new XElement("Firma", reader[15]));
                                if (reader[16] != "NULL") 
                                person.Add(new XElement("FirmaID", reader[16]));
                                output.Add(person);
                            break;
                        case 1:
                            XElement firma = new XElement("Firma");
                            firma.Add(new XElement("ID", reader[0]));
                            firma.Add(new XElement("Adresse", reader[1]));
                            firma.Add(new XElement("PLZ", reader[2]));                         
                            firma.Add(new XElement("Ort", reader[3]));                     
                                if (reader[4] != "NULL")
                                    firma.Add(new XElement("RechnungsAdresse", reader[4]));
                                if (reader[5] != "NULL")
                                    firma.Add(new XElement("RechnungsPLZ", reader[5]));
                                if (reader[6] != "NULL")
                                    firma.Add(new XElement("RechnungsOrt", reader[6]));
                                if (reader[7] != "NULL")
                                    firma.Add(new XElement("LieferAdresse", reader[7]));
                                if (reader[8] != "NULL")
                                    firma.Add(new XElement("LieferPLZ", reader[8]));
                                if (reader[9] != "NULL")
                                    firma.Add(new XElement("LieferOrt", reader[9]));                                
                            firma.Add(new XElement("Name", reader[10]));
                            firma.Add(new XElement("UID", reader[11]));
                                output.Add(firma);
                            break;
                        case 2:
                            XElement rechnung = new XElement("Rechnung");
                            rechnung.Add(new XElement("ID", reader[0]));
                            rechnung.Add(new XElement("Kundenname", reader[1]));
                            rechnung.Add(new XElement("KundenID", reader[2]));
                            rechnung.Add(new XElement("Date", Convert.ToDateTime(reader[3]).ToString("dd/MM/yyyy")));
                            rechnung.Add(new XElement("Due", Convert.ToDateTime(reader[4]).ToString("dd/MM/yyyy")));
                            rechnung.Add(new XElement("Kommentar", reader[5]));
                            rechnung.Add(new XElement("Nachricht", reader[6]));
                            output.Add(rechnung);
                            break;
                        case 3:
                            XElement firm = new XElement("Firma");
                            firm.Add(new XElement("ID", reader[0]));
                            firm.Add(new XElement("Name", reader[1]));
                            output.Add(firm);
                            break;
                        case 4:
                            XElement zeile = new XElement("Zeile");
                            zeile.Add(new XElement("Artikel", reader[0]));
                            zeile.Add(new XElement("Stk", reader[1]));
                            zeile.Add(new XElement("Preis", reader[2]));
                            output.Add(zeile);
                            break;
                        case 5:
                            XElement pers = new XElement("Person");
                            pers.Add(new XElement("ID", reader[0]));
                            pers.Add(new XElement("Vorname", reader[1]));
                            pers.Add(new XElement("Nachname", reader[2]));
                            output.Add(pers);
                            break;
                        default:
                            break;
                    }                    
                }
            }
            finally
            {
                reader.Close();
            }
            return output.ToString();
        }
    }
}
