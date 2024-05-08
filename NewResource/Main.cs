using DbModels;
using GTANetworkAPI;
using Services;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        public async void Event_OnPlayerConnect(Player player)
        {
            NAPI.ClientEvent.TriggerClientEvent(player, "OnPlayerConnected", true);

            await new MechanicService().GetVehicleServices(player);
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
