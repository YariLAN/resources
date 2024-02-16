using DbModels;
using GTANetworkAPI;
using System;
using System.Linq;

namespace Mappers
{
    public class VehicleModelMapper
    {
        public VehicleModel ToMap(Player player, uint veh, int colOne, int colTwo, double price)
        {
            var pos = player.Position.Around(5);

            return new VehicleModel()
            {
                OwnerID = player.GetData<int>("ID"),
                Model = (int)veh,
                Number = GenerateNumber(),
                OldPosition = new float[] { pos.X, pos.Y, pos.Z },
                ColorOne = colOne,
                ColorTwo = colTwo,
                Price = price
            };
        }

        private string GenerateNumber()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var random = new Random();

            return new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
