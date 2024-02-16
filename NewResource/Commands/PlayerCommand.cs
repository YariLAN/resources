using DbModels;
using GTANetworkAPI;
using Services;
using System.Threading.Tasks;

namespace NewResource.Commands
{
    public class PlayerCommand : Script
    {
        [Command("name")]
        public async Task SetName(Player player, string name)
        {
            await new PlayerService().SetName(player, name);
        }

        [Command("setadmin")]
        public async Task SetAdminRight(Player player)
        {
            await new PlayerService().SetAdmin(player);
        }

        [Command("veh")]
        public async Task GenerateVehicle(Player player, string model, int colorOne = 0, int colorTwo = 0)
        {
            await new PlayerService().GetVehicle(player, model);    
        }

        [ServerEvent(Event.PlayerExitVehicle)]
        public void Event_OnExitVehicle(Player player, Vehicle vehicle)
        {
            new PlayerService().AdminExitVehicleEvent(player, vehicle);
        }

        [ServerEvent(Event.PlayerEnterVehicle)]
        public void Event_OnEnterVehicle(Player player, Vehicle vehicle, sbyte seat)
        {
            new PlayerService().EnterVehicleEvent(player, vehicle);
        }

        [Command("accept")]
        public async Task AcceptSell(Player buyer)
        {
            await new PlayerService().AcceptSell(buyer);
        }
    }
}
