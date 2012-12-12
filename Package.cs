using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TictactoeServer
{
    class Package
    {
        public string Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Data { get; set; }

        public Package() { }

        public Package(string type, string from, string to, string data)
        {
            Type = type;
            From = from;
            To = to;
            Data = data;
        }
    }
}
