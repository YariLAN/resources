using DbModels;
using GTANetworkAPI;
using Services;
using System;
using System.Threading.Tasks;

namespace NewResource.Commands
{
    public class VehicleCommands : Script
    {
        [Command("vehicles/my")]
        public async Task GetVehiclesByPlayer(Player player)
        {
            await new VehiclesService().GetVehiclesByPlayer(player);
        }

        [Command("vehicles/setprice")]
        public async Task SetVehiclePrice(Player player, string model, double price)
        {
            await new VehiclesService().SetVehiclePrice(player, model, price);
        }

        [Command("vehicles")]
        public async Task GetVehicles(Player player)
        {
            await new VehiclesService().GetVehilcesAsync(player);
        }

        [Command("vehicles/buy")]
        public async Task BuyVehicle(Player player, string model, int colorOne, int colorTwo)
        {
            await new VehiclesService().BuyVehicleAsync(player, model, colorOne, colorTwo);
        }

        [Command("vehicles/my/spawn")]
        public async Task SpawnVehicle(Player player, string model)
        {
            await new VehiclesService().SpawnAsync(player, model);
        }

        [Command("vehicles/my/despawn")]
        public async Task DespawnVehicle(Player player, int id)
        {
            await new VehiclesService().DespawnAsync(player, id);
        }

        [Command("park")]
        public async Task ParkVehicle(Player player)
        {
            await new VehiclesService().ParkAsync(player);
        }

        [Command("vehicles/my/sell_player")]
        public async Task SellPlayerVehicle(Player player, string buyerName, int vehicleId, double price)
        {
            await new VehiclesService().SellToPlayer(player, buyerName, vehicleId, price);    
        }

        [Command("vehicles/my/sell")]
        public async Task SellVehicle(Player player, int vehicleId)
        {
            await new VehiclesService().SellToCountry(player, vehicleId);   
        }
    }
}
