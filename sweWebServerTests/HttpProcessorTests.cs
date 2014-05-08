using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sweWebServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace sweWebServer.Tests
{
    [TestClass()]
    public class HttpProcessorTests
    {
        [TestMethod()]
        public void HttpProcessorTest()
        {
            webServer server = new webServer(9090);
            System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
            HttpProcessor proc = new HttpProcessor(client, server);
            Assert.IsNotNull(proc);
        }
    }
}
