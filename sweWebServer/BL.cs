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
        string statement;

        public BL()
        {
            access = new DAL();
        }

        public string DoRequest(string param)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(param);

            XmlElement root = xml.DocumentElement;
            XmlNode type = root.ChildNodes[0];

            #region Search
            if (root.Name == "Search")
            {
                if (type.Name == "Person")
                {
                    XmlNodeList xnList = type.ChildNodes;

                    if (xnList.Count > 1)
                    {
                        statement = "SELECT * FROM Kontakte WHERE Vorname = @vorname AND Nachname = @nachname";
                        cmd.Parameters.Add("@vorname", xnList[0].Value);
                        cmd.Parameters.Add("@nachname", xnList[1].Value);
                    }

                    else
                    {
                        if (type.FirstChild.Name == "Vorname")
                        {
                            statement = "SELECT * FROM Kontakte WHERE Vorname = @vorname";
                            cmd.Parameters.Add("@vorname", xnList[0].Value);
                        }

                        else
                        {
                            statement = "SELECT * FROM Kontakte WHERE Nachname = @nachname";
                            cmd.Parameters.Add("@nachname", xnList[0].Value);
                        }
                    }
                }
                else if (type.Name == "Firma")
                {
                    XmlNode search = type.FirstChild;

                    switch (search.Name)
                    {
                        case "Name":
                            statement = "SELECT * FROM Kontakte WHERE Name = @name";
                            cmd.Parameters.Add("@name", search.Value);
                            break;
                        case "UID":
                            statement = "SELECT * FROM Kontakte WHERE UID = @uid";
                            cmd.Parameters.Add("@uid", search.Value);
                            break;
                        default:
                            break;
                    }
                }
                else if (type.Name == "Rechnung")
                {
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

            return access.testtest("ghgh");
        }
    }
}