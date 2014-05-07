using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

namespace sweWebServer
{
    public class DAL
    {
        SqlConnection connection;
        XmlDocument xml;
        SqlCommand cmd;
        string statement;       

        public DAL()
        {
            xml = new XmlDocument();            

            if (openConnection())
                openConnection();
        }

        public bool openConnection()
        {
            try
            {
                connection = new SqlConnection(
                    @"Data Source=.\SqlExpress;
                    Initial Catalog=SweDB;
                    Integrated Security=true;");

                connection.Open();
                cmd = new SqlCommand(statement, connection);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string testtest(string data)
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

        public void insert()
        {
            statement = "INSERT INTO table (name, information, other) VALUES (@name, @information, @other)";
            cmd.Parameters.Add("@name", "test");
            cmd.Parameters.Add("@information","test");
            cmd.Parameters.Add("@other", "test");
            cmd.ExecuteNonQuery();
        }

        public void update()
        { }

        public void delete()
        { }

        public string select(SqlCommand command, int type)
        {
            command.Connection = connection;
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
                                output.Add(person);
                            break;
                        case 1:
                            XElement firma = new XElement("Firma");
                            firma.Add(new XElement("ID", reader[0]));
                            firma.Add(new XElement("Adresse", reader[5]));
                            firma.Add(new XElement("PLZ", reader[6]));                         
                            firma.Add(new XElement("Ort", reader[7]));
                            firma.Add(new XElement("Name", reader[15]));
                            firma.Add(new XElement("UID", reader[16]));
                                output.Add(firma);
                            break;
                        case 2:
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
