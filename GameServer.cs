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
        private Thread Serverthread;
        private Thread ClientThread;
        public bool IsRunning {get;set;}

        public GameServer(string ip, int port)
        {
            IsRunning = false;
            listener = new TcpListener(IPAddress.Parse(ip), port);
        }

        /// <summary>
        /// Checks if the server is running or not and
        /// starts the server loop and server thread if there is no server running.
        /// Make sure the thread is a background thread so it really ends when we close the application
        /// </summary>
        public void StartServer()
        {
            if (IsRunning)
            {
                MessageBox.Show("The server is already online");
            }
            else
            {
                IsRunning = true;
                listener.Start();
                Serverthread = new Thread(new ThreadStart(ListenForClients));
                Serverthread.IsBackground = true;
                Serverthread.Start();
                Console.WriteLine("Server started");
                MessageBox.Show("Server started!");
            }
        }

        /// <summary>
        /// Start a new thread for each client that is connected.
        /// Then adds that client to the ClientSocket list
        /// </summary>
        private void ListenForClients()
        {
            try
            {
                while(IsRunning)
                {
                    // Check if the listener is pending so we can stop it later
                    if (!listener.Pending())
                    {
                        Thread.Sleep(500);
                        continue;
                    }

                    Socket ConnectedClientSocket = listener.AcceptSocket();
                    // When the tcpListener gets a new connection inject the socket into a new ClientSocket object
                    // and start a new thread for that ClientSocket. 
                    ClientSocket client = new ClientSocket(ConnectedClientSocket);
                    ClientThread = new Thread(new ThreadStart(client.ReadSocket));
                    ClientThread.IsBackground = true;
                    ClientThread.Start();
                    ClientSockets.Add(client);
                    // Make sure the ClientSocket has all a list of all the other connected sockets.
                    UpdateAllClientLists();
                    Console.WriteLine("Client Connected: " + ConnectedClientSocket.RemoteEndPoint);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("server: " + e.Message);   
            }
        }

        private void UpdateAllClientLists()
        {
            foreach (ClientSocket cs in ClientSockets)
            {
                cs.UpdateClientList(ClientSockets);
            }
        }

        /// <summary>
        /// Stops the listener and terminates the main server thread gracefully.
        /// </summary>
        public void StopServer()
        {
            if(!IsRunning)
            {
                MessageBox.Show("the server is not running");
            }
            else
            {
                IsRunning = false; // kill the server loop
                listener.Stop(); // Stop the tcp listener
                Serverthread.Join(); // Join the thread so it terminates gracefully

                // Empty the ClientSockets list and close each socket, do this after the serverthread is gone to avoid deadlock
                foreach (ClientSocket cs in ClientSockets.ToList())
                {
                    cs.EndClientSocket();
                    ClientSockets.Remove(cs);
                }

                Console.WriteLine("server stopped");
                MessageBox.Show("Server stopped!");
            }
        }
    }
}
