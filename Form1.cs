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
    /// <summary>
    /// TicTacToe Server
    /// Tord Munk
    /// 
    /// ABOUT
    ///     This is the game server to the TicTacToe client.
    ///     It has a few bugs and everything is not implemented yet.
    ///     But basic gameplay is working.
    ///     
    ///LICENSE:
    ///     gpl3
    ///     
    /// </summary>
    public partial class Form1 : Form
    {
        GameServer myGameServer = new GameServer("127.0.0.1",8888);
        public static ListBox ClientListref = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myGameServer.StartServer();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
