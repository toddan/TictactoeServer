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
        /// <summary>
        /// Server task factory.
        /// Depending of what kind of task the client wants the server to do. It return a new ITask object.
        /// Each ITask object dose one or several tasks that the client demands. 
        /// </summary>
        /// <param name="package">Package sent by the client</param>
        /// <param name="clientList">List of connected clients</param>
        /// <returns>ITask object</returns>
        public ITask GetTaskObject(Package package,List<ClientSocket> clientList)
        {
            switch (package.Type)
            {
                case "login":
                    return new LoginTaskObj(package,clientList);
                case "newgame":
                    return new NewGameTaskObj(package, clientList);
                case "gamepattern":
                    return new GamePatternTaskObj(package, clientList);
            }

            return null;
        }
    }
}
