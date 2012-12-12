using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TictactoeServer
{
    public partial class Form1 : Form
    {
        GameServer myGameServer = new GameServer("127.0.0.1",8888);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myGameServer.StartServer();
        }
        public void UpdateClientList()
        {
            string[] ClientList = myGameServer.GetAllContectedClients().Split(';');
            listBox1.DataSource = ClientList;
            listBox1.Update();
        }
    }
}
