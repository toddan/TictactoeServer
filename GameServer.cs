using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TictactoeServer
{
    class GameServer
    {
        private TcpListener listener;

        private List<ClientSocket> ClientSockets = new List<ClientSocket>();

        public GameServer(string ip, int port)
        {
            listener = new TcpListener(IPAddress.Parse(ip), port);
            listener.Start();
        }

        public void StartServer()
        {
            Thread Serverthread = new Thread(new ThreadStart(ListenForClients));
            Serverthread.Start();
        }

        private void ListenForClients()
        {
            try
            {
                for (; ;)
                {
                    Socket ConnectedClientSocket = listener.AcceptSocket();
                    ClientSocket client = new ClientSocket(ConnectedClientSocket);
                    Thread ClientThread = new Thread(new ThreadStart(client.ReadSocket));
                    ClientThread.Start();
                    ClientSockets.Add(client);
                    client.UpdateClientList(ClientSockets);
                    MessageBox.Show("Client Connected: " + ConnectedClientSocket.RemoteEndPoint);
                }
            }
            catch (Exception e)
            {
                foreach (ClientSocket cs in ClientSockets)
                {
                    cs.EndClientSocket();
                }
                MessageBox.Show(e.Message);
            }
        }

        public string GetAllContectedClients()
        {
            string clients = "";
            foreach (ClientSocket cs in ClientSockets)
            {
                clients += cs.ClientInfo + ';';
            }
            return clients;
        }
    }
}
