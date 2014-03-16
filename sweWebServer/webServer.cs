using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace sweWebServer
{
    public class webServer
    {
        protected int port;
        TcpListener listener;
        bool is_active = true;
 
            public webServer(int port)
            {
                this.port = port;
            }
 
            public void listen()
            {
                listener = new TcpListener(IPAddress.Loopback, port);
                listener.Start();

                while (is_active)
                {
                    TcpClient s = listener.AcceptTcpClient();
                    HttpProcessor processor = new HttpProcessor(s, this);
                    Thread thread = new Thread(new ThreadStart(processor.process));
                    thread.Start();
                    Thread.Sleep(1);
                }
            }

           /* public void handleGETRequest(HttpProcessor p)
            {

                if (p.http_url.Equals("/Test.png"))
                {
                    Stream fs = File.Open("../../Test.png", FileMode.Open);

                    p.writeSuccess("image/png");
                    fs.CopyTo(p.outputStream.BaseStream);
                    p.outputStream.BaseStream.Flush();
                }

                Console.WriteLine("request: {0}", p.http_url);
                p.writeSuccess();
                p.outputStream.WriteLine("<html><body><h1>test server</h1>");
                p.outputStream.WriteLine("Current Time: " + DateTime.Now.ToString());
                p.outputStream.WriteLine("url : {0}", p.http_url);

                p.outputStream.WriteLine("<form method=post action=/form>");
                p.outputStream.WriteLine("<input type=text name=foo value=foovalue>");
                p.outputStream.WriteLine("<input type=submit name=bar value=barvalue>");
                p.outputStream.WriteLine("</form>");
            }

            public void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
            {
                Console.WriteLine("POST request: {0}", p.http_url);
                string data = inputData.ReadToEnd();

                p.writeSuccess();
                p.outputStream.WriteLine("<html><body><h1>test server</h1>");
                p.outputStream.WriteLine("<a href=/test>return</a><p>");
                p.outputStream.WriteLine("postbody: <pre>{0}</pre>", data);
            }*/
    }
}
