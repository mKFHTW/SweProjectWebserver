using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace sweWebServer
{
    public interface IPlugins
    {
        string pluginName { get; }
        void handleRequest(StreamWriter outStream, HTTPHeader head);
    }
}
