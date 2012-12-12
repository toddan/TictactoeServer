using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TictactoeServer
{
    static class PacketParser
    {
        public static string MakePackageString(Package package)
        {
            string PackageString = "";
            PackageString += "<type>" + package.Type + "</type>";
            PackageString += "<to>" + package.To + "</to>";
            PackageString += "<from>" + package.From + "</from>";
            PackageString += "<data>" + package.Data + "</data>";
            return PackageString;
        }

        public static Package ParsePackageString(string packageString)
        {
            Package myPackage = new Package();

            char[] paths = {'<','>'};
            string[] commands = packageString.Split(paths);

            for(int i = 0; i < commands.Length; i++)
            {
                if (commands[i] == "type")
                {
                    myPackage.Type = commands[i + 1];
                }
                if (commands[i] == "to")
                {
                    myPackage.To = commands[i + 1];
                }
                if (commands[i] == "from")
                {
                    myPackage.From = commands[i + 1];
                }
                if (commands[i] == "data")
                {
                    myPackage.Data = commands[i + 1];
                }
            }

            return myPackage;
        }
    }
}
