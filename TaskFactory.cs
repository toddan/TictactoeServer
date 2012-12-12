using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TictactoeServer
{
    class TaskFactory
    {
        public ITask GetTaskObject(Package package,List<ClientSocket> clientList)
        {
            switch (package.Type)
            {
                case "login":
                    return new LoginTaskObj(package,clientList);
            }

            return null;
        }
    }
}
