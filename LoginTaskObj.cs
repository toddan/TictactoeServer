using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TictactoeServer
{
    class LoginTaskObj : ITask
    {
        private List<ClientSocket> ClientList = new List<ClientSocket>();

        private Package LoginPackage;

        public LoginTaskObj(Package loginPackage,List<ClientSocket> clientList)
        {
            LoginPackage = loginPackage;
            ClientList = clientList;
        }

        public void DoTask()
        {
            SendToAllClients();
        }

        /// <summary>
        /// When a new client logins we want to update.
        /// Every client with a new client list.
        /// </summary>
        private void SendToAllClients()
        {
            foreach (ClientSocket cs in ClientList)
            {
                cs.WriteToSocket(MakeListOfClients());
            }
        }

        private string MakeListOfClients()
        {
            string Clients = "";

            LoginPackage = new Package();
            LoginPackage.Type = "userlist";
            LoginPackage.From = "server";
            LoginPackage.To = "all";

            foreach (ClientSocket cs in ClientList)
            {
                Clients += cs.ClientIp + ";";
            }

            LoginPackage.Data = Clients;

            return PacketParser.MakePackageString(LoginPackage); ;
        }
    }
}
