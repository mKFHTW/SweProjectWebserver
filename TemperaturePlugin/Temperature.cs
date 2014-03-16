using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using sweWebServer;


 
//namespace sweWebServer
namespace TemperaturePlugin 
{
    public class Temperature : IPlugins
    {
        private bool active = false;
        private SqlConnection SQLCon;
        private Dictionary<string, string> ourDict = new Dictionary<string,string>();
        private int a = 0;
        private string res = "";
        private string html;

        private string mypluginName = "Search";

        public bool openSQLCon()
        {
            try
            {
                SQLCon = new SqlConnection(
                    @"Data Source=.\SqlExpress;
                    Initial Catalog=sweWebserver;
                    Integrated Security=true;");

                SQLCon.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void handleRequest(StreamWriter writeOut, HTTPHeader head)
        {
              try
                {

                    /*openSQLCon();


                string command = "SELECT * FROM tempTbl ORDER BY date DESC";

                SqlCommand cmd = new SqlCommand(command, SQLCon);

                

                SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ourDict.Add(reader[0].ToString(), reader[1].ToString());
                        //string[] dayparts = day.Split(' ');

                    }
                 
                  if (head.UrlSubStrings.Count() == 2)
                    {
                
                    for (int i = 0; i < 10; i++)
                    {
                        res += "<tr><td>" + ourDict.ElementAt(i).Key + "</td><td>" + ourDict.ElementAt(i).Value + "</td></tr>";
                    }


                    string html = @"
<html>
    <head>
        <title>SWE Webserver Gruppe SEVILMIS - KAVLAK</title>
    </head>
    <body>
        <h1>Temperatures</h1>
        <table border='1'>
            <tr><th>Day</th><th>Temperature</th></tr>" +
                        res +
                    @"</table>
        <a href='http://localhost:8080/TemperaturePlugin/Prev/0'>Prev</a> <a href='http://localhost:8080/TemperaturePlugin/Next/0'>Next</a>
        <br>
        <p><a href='http://localhost:8080/'>Default</a></p>
    </body>
</html>";

                    writeOut.WriteLine("HTTP/1.1 200 OK");
                    writeOut.WriteLine("Content-Type: text/html");
                    writeOut.WriteLine("Content-Length: " + html.Length);
                    writeOut.WriteLine("Connection: close");
                    writeOut.Write(System.Environment.NewLine);
                    writeOut.WriteLine(html);

               
            }

            else
            {
                      if(head.UrlSubStrings.Count()>3)
                      {
                          a= Convert.ToInt32(head.UrlSubStrings[3]);
                      }

                if (head.UrlSubStrings[2] == "Prev")
                {
                    if (a != 0)
                    {
                       a -= 10;
                        
                    }
                }
                if (head.UrlSubStrings[2] == "Next")
                {
                    if (a + 10 >= ourDict.Count)
                        a += ourDict.Count % 10;
                    else 
                        a += 10;
                    
                }

                for (int i = a; i < (a + 10); i++)
                {
                    res += "<tr><td>" + ourDict.ElementAt(i).Key + "</td><td>" + ourDict.ElementAt(i).Value + "</td></tr>";
                }

                html = @"
                    <html>
                        <head>
                            <title>SWE Webserver Gruppe SEVILMIS - KAVLAK</title>
                        </head>
                        <body>
                            <h1>Temperatures</h1>
                            <table border='1'>
                                <tr><th>Day</th><th>Temperature</th></tr>" +
                                      res +
                                  @"</table>
                            <a href='http://localhost:8080/TemperaturePlugin/Prev/" + a +
     @"'>Prev</a> 
                            <a href='http://localhost:8080/TemperaturePlugin/Next/" + a +
     @"'>Next</a>
                            <br>
                            <p><a href='http://localhost:8080/'>Default</a></p>
                        </body>
                    </html>";*/

                    html = "geht";
                writeOut.WriteLine("HTTP/1.1 200 OK");
                writeOut.WriteLine("Content-Type: text/html");
                writeOut.WriteLine("Content-Length: " + html.Length);
                writeOut.WriteLine("Connection: close");
                writeOut.Write(System.Environment.NewLine);
                writeOut.WriteLine(html);
            }

                //}
              catch (Exception ex)
              {
                  Console.WriteLine(ex.Message);
                  SQLCon.Close();
              }
        }

        public string pluginName
        {
            get
            {
                return mypluginName;
            }
        }
    }
}
