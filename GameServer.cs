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

        /// <summary>
        /// Starts the main server thread that listens for new clients.
        /// </summary>
        public void StartServer()
        {
            Thread Serverthread = new Thread(new ThreadStart(ListenForClients));
            Serverthread.Start();
            Console.WriteLine("Server started");
        }

        /// <summary>
        /// Start a new thread for each client that is connected.
        /// Then adds that client to the ClientSocket list
        /// </summary>
        private void ListenForClients()
        {
            try
            {
                for (; ;)
                {
                    Socket ConnectedClientSocket = listener.AcceptSocket();
                    // When the tcpListener gets a new connection inject the socket into a new ClientSocket object
                    // and start a new thread for that ClientSocket. 
                    ClientSocket client = new ClientSocket(ConnectedClientSocket);
                    Thread ClientThread = new Thread(new ThreadStart(client.ReadSocket));
                    ClientThread.Start();
                    ClientSockets.Add(client);
                    // Make sure the ClientSocket has all a list of all the other connected sockets.
                    UpdateAllClientLists();
                    Console.WriteLine("Client Connected: " + ConnectedClientSocket.RemoteEndPoint);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("server: " + e.Message);   
            }
        }

        private void UpdateAllClientLists()
        {
            foreach (ClientSocket cs in ClientSockets)
            {
                cs.UpdateClientList(ClientSockets);
            }
        }
    }
}
