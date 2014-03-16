using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
using System.Windows;

namespace sweWebServer
{
    public class HttpProcessor
    {
        public TcpClient socket;
        public webServer srv;
        private HTTPHeader myHead;
        private StreamReader inputStream;
        public StreamWriter outputStream;
        private ArrayList _loadedPlugins;        
        public Hashtable httpHeaders = new Hashtable();

        int ContentLength;

        public HttpProcessor(TcpClient s, webServer srv)
        {
            this.socket = s;
            this.srv = srv;
            myHead = new HTTPHeader();
        }
        
        public void process()
        {
            inputStream = new StreamReader(socket.GetStream());            
            outputStream = new StreamWriter(new BufferedStream(socket.GetStream()));

            try
            {
                parseRequest();
                readHeaders();
                
                char[] ClientInput = new char[ContentLength];

                inputStream.ReadBlock(ClientInput, 0, ContentLength);                                   
                string html = "OK";

                outputStream.WriteLine("HTTP/1.1 200 OK");
                outputStream.WriteLine("Content-Type: text/html");
                outputStream.WriteLine("Content-Length: " + html.Length);
                outputStream.WriteLine("Connection: close");
                outputStream.Write(System.Environment.NewLine);
                outputStream.WriteLine(html);

                /*pluginMngr PluginManager = new pluginMngr();
                _loadedPlugins = PluginManager.LoadPlugins("/plugins/", "*.dll", typeof(sweWebServer.IPlugins));

                bool tried = false;

                foreach (IPlugins plug in _loadedPlugins)
                {
                    //if (plug.pluginName == WebRequest.RequestedPlugin)
                        if (plug.pluginName == myHead.UrlSubStrings[1])
                    {
                        tried = true;
                        Console.WriteLine("requested plugin: " + plug.pluginName);

                        plug.handleRequest(outputStream, myHead);
                       
                    }
                }*/               
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
              
            }

            outputStream.Flush();            
            inputStream = null; 
            outputStream = null;            
            socket.Close();
        }

        public void parseRequest()
        {
            String request = inputStream.ReadLine();
            string[] tokens = null;
            try
            {
                tokens = request.Split(' ');

                if (tokens.Length != 3)
                {
                    throw new Exception("invalid http request line");
                }
                myHead.Http_method = tokens[0].ToUpper();
                myHead.Http_url = tokens[1];
                myHead.Http_protocol_versionstring = tokens[2];

                myHead.splitURL();

                //Console.WriteLine("starting: " + request);
            }
            catch (Exception)
            {

            }
        }

        public void readHeaders()
        {
            //Console.WriteLine("readHeaders()");
            String line;
            
            while ((line = inputStream.ReadLine()) != null)
            {
                if (line.Equals(""))
                {
                    //Console.WriteLine("got headers");
                    return;
                }

                int separator = line.IndexOf(':');
                if (separator == -1)
                {
                    throw new Exception("invalid http header line: " + line);
                }

                String name = line.Substring(0, separator);
                int pos = separator + 1;

                while ((pos < line.Length) && (line[pos] == ' '))
                {
                    pos++;
                }

                string value = line.Substring(pos, line.Length - pos);
                //Console.WriteLine("header: {0}:{1}", name, value);
                httpHeaders[name] = value;

                if (name == "Content-Length")                
                    ContentLength = Convert.ToInt32(value);                
            }
        }   
    }
}
