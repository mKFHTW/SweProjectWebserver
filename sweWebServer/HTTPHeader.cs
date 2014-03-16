using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sweWebServer
{
    public class HTTPHeader
    {
        private String http_method;
        private String http_url;
        private String http_protocol_versionstring;
        private string[] urlSubStrings;

        public String Http_method { get { return http_method; } set { http_method = value; } }
        public String Http_url { get { return http_url; } set { http_url = value; } }
        public String Http_protocol_versionstring { get { return http_protocol_versionstring; } set { http_protocol_versionstring = value; } }
        public string[] UrlSubStrings { get { return urlSubStrings; } }
            
        public void splitURL()
        {
            urlSubStrings = http_url.Split('/');
        }
    }
}
