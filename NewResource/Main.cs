using DbModels;
using GTANetworkAPI;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewResource
{
    public class Main : Script
    {
        public static List<int> LoggedInIDs = new List<int>();

        public Main()
        {
        }

        [ServerEvent(Event.PlayerConnected)]
        public void Event_OnPlayerConnect(Player player)
        {
            player.SendChatMessage("Welcome! Please register or logged in if was sign up");
        }

        [ServerEvent(Event.PlayerDisconnected)]
        public void Event_OnDisconnect(Player player, DisconnectionType type, string reason)
        {
            LoggedInIDs.Remove(player.GetData<int>("ID"));

            new DisconnectService().ExecuteAsync(player);
        }

        [Command("dc")]
        public void CMD_Disconnect(Player player)
        {
            player.Kick("DC");
        }
    }
}
