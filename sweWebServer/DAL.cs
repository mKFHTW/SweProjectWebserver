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
        //string test;

        public DAL()
        {
            xml = new XmlDocument();
            cmd = new SqlCommand(statement, connection);
            /*if (openConnection())
                openConnection();*/
        }

        public bool openConnection()
        {
            try
            {
                connection = new SqlConnection(
                    @"Data Source=.\SqlExpress;
                    Initial Catalog=sweDB;
                    Integrated Security=true;");

                connection.Open();
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
            cmd.Parameters.Add("@name", test);
            cmd.Parameters.Add("@information", test);
            cmd.Parameters.Add("@other", test);
            cmd.ExecuteNonQuery();
        }

        public void update()
        { }

        public void delete()
        { }

        public void select()
        { }
    }
}
