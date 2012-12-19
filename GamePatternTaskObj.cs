using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TictactoeServer
{
    class GamePatternTaskObj : ITask
    {
        private List<ClientSocket> ClientList = new List<ClientSocket>();

        private Package GamePatternPackage;

        public GamePatternTaskObj(Package gamePatternPackage, List<ClientSocket> clientList)
        {
            GamePatternPackage = gamePatternPackage;
            ClientList = clientList;
        }

        public void DoTask()
        {
            SendGamePatternToOpponent();
        }

        /// <summary>
        /// Finds and returns the opponents socket.
        /// </summary>
        /// <returns>ClientSocket object</returns>
        private ClientSocket FindOpponent()
        {
            foreach (ClientSocket cs in ClientList)
            {
                if (cs.ClientIp == GamePatternPackage.To)
                {
                    return cs;
                }
            }

            return null;
        }

        private void SendGamePatternToOpponent()
        {
            ClientSocket Opponent = FindOpponent();
            Opponent.WriteToSocket(PacketParser.MakePackageString(new Package("gamepattern", GamePatternPackage.From, 
                GamePatternPackage.To, GamePatternPackage.Data))); 
        }
    }
}
