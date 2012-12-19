using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TictactoeServer
{
    class NewGameTaskObj : ITask
    {
        private List<ClientSocket> ClientList = new List<ClientSocket>();

        private Package GamePackage;

        public NewGameTaskObj(Package gamePackage,List<ClientSocket> clientList)
        {
            GamePackage = gamePackage;
            ClientList = clientList;
        }

        public void DoTask()
        {
            SendGameInviteToOpponent();
        }

        /// <summary>
        /// Finds and returns the opponents socket.
        /// </summary>
        /// <returns>ClientSocket object</returns>
        private ClientSocket FindOpponent()
        {
            foreach (ClientSocket cs in ClientList)
            {
                if(cs.ClientIp == GamePackage.To)
                {
                    return cs;
                }
            }

            return null;
        }

        private void SendGameInviteToOpponent()
        {
            ClientSocket Opponent = FindOpponent();
            Opponent.WriteToSocket(PacketParser.MakePackageString(new Package("newgame",GamePackage.From,GamePackage.To,GamePackage.Data)));
        }

    }
}
