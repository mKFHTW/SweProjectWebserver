using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Data.SqlClient;

namespace sweWebServer
{
    class BL
    {
        DAL access;
        SqlCommand cmd;
        string statement = null;
        

        public BL()
        {
            access = new DAL();
            cmd = new SqlCommand();
        }

        public string DoRequest(string param)
        {
            int searchType = 0;
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(param);

            XmlElement root = xml.DocumentElement;
            XmlNode type = root.ChildNodes[0];

            #region Search
            if (root.Name == "Search")
            {
                if (type.Name == "Person")
                {
                    searchType = 0;
                    XmlNodeList xnList = type.ChildNodes;

                    if (xnList.Count > 1)
                    {
                        statement = "SELECT * FROM Kontaktdaten WHERE Vorname = @vorname AND Nachname = @nachname";
                        cmd.Parameters.AddWithValue("@vorname", type.FirstChild.InnerText);
                        cmd.Parameters.AddWithValue("@nachname", type.FirstChild.InnerText);
                    }

                    else
                    {
                        if (type.FirstChild.Name == "Vorname")
                        {
                            statement = "SELECT * FROM Kontaktdaten WHERE Vorname = @vorname";
                            cmd.Parameters.AddWithValue("@vorname", type.FirstChild.InnerText);
                        }

                        else
                        {
                            statement = "SELECT * FROM Kontaktdaten WHERE Nachname = @nachname";
                            cmd.Parameters.AddWithValue("@nachname", type.FirstChild.InnerText);
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
                            statement = "SELECT * FROM Kontaktdaten WHERE Firmenname = @name";
                            cmd.Parameters.AddWithValue("@name", search.InnerText);
                            break;
                        case "UID":
                            statement = "SELECT * FROM Kontaktdaten WHERE UID = @uid";
                            cmd.Parameters.AddWithValue("@uid", search.InnerText);
                            break;
                        default:
                            break;
                    }
                }
                else if (type.Name == "Rechnung")
                {
                    searchType = 2;
                    XmlNodeList xnList = type.SelectNodes("/");

                    foreach (XmlNode xn in xnList)
                    {
                    }
                }

                
            }
            #endregion

            else if (root.Name == "Insert")
            {

            }
            else if (root.Name == "Update")
            {

            }
            else if (root.Name == "Delete")
            {

            }
            cmd.CommandText = statement;
            return access.select(cmd, searchType);
        }
    }
}