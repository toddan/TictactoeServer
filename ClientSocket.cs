using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TictactoeServer
{
    class ClientSocket
    {
        private Socket myClientSocket;
        private StreamReader ClientStreamReader;
        private StreamWriter ClientStreamWriter;
        private Stream ClientStream;

        private List<ClientSocket> ClientList = new List<ClientSocket>();

        public string ClientIp
        {
            get
            {
                return myClientSocket.RemoteEndPoint.ToString();
            }
        }

        public ClientSocket(Socket socket)
        {
            myClientSocket = socket;
            ClientStream = new NetworkStream(myClientSocket);
            ClientStreamReader = new StreamReader(ClientStream);
            ClientStreamWriter = new StreamWriter(ClientStream);
        }

        /// <summary>
        /// Read the data sent by the client thru the clientSocket.
        /// </summary>
        public void ReadSocket()
        {
            try
            {
                for (; ;)
                {
                    string message = ClientStreamReader.ReadLine();
                    Console.WriteLine("server: " + message);
                    HandleClientTask(PacketParser.ParsePackageString(message));
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                EndClientSocket();
            }
        }

        /// <summary>
        /// Creates a new task object for each new task the client sends
        /// to the server. Then run the DoTask method
        /// </summary>
        /// <param name="package">Package sent by the client</param>
        private void HandleClientTask(Package package)
        {
            TaskFactory ServerTaskFactory = new TaskFactory();
            ITask ServerTask = ServerTaskFactory.GetTaskObject(package,ClientList);
            ServerTask.DoTask();
        }

        public void WriteToSocket(string data)
        {
            ClientStreamWriter.AutoFlush = true;
            ClientStreamWriter.WriteLine(data);
        }

        public void EndClientSocket()
        {
            myClientSocket.Close();
            ClientList.Remove(this);
        }

        public void UpdateClientList(List<ClientSocket> ServerClientList)
        {
            ClientList = ServerClientList;
        }
    }
}
