using DbModels;
using GTANetworkAPI;
using Mappers;
using Provider;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class VehiclesService
    {
        private readonly VehicleModelRepository _vehicleRepository;
        private readonly VehiclePriceRepository _vehiclePriceRepository;
        private readonly UserModelRepository _userModelRepository;

        public VehiclesService()
        {
            _vehicleRepository = new VehicleModelRepository();
            _vehiclePriceRepository = new VehiclePriceRepository();
            _userModelRepository = new UserModelRepository(new RageDbContext());
        }

        public static List<int> SpawnedVehicles { get; set; } = new List<int>();

        public async Task GetVehiclesByPlayer(Player player)
        {
            if (!player.HasData("ID"))
            {
                player.SendChatMessage("Вы не авторизованы");
                return;
            }

            int id = player.GetData<int>("ID");

            List<VehicleModel> vehicles = await _vehicleRepository.GetVehiclesByOwnerIdAsync(id);

            if (vehicles.Count == 0)
            {
                player.SendChatMessage("У вас нет машин");
                return;
            }

            foreach (var vehicle in vehicles)
            {
                player.SendChatMessage($"{vehicle.Id} --> {(VehicleHash)vehicle.Model}");
            }
        }

        public async Task GetVehilcesAsync(Player player)
        {
            var list = await _vehiclePriceRepository.GetValuesAsync();

            foreach (var vehicle in list)
            {
                player.SendChatMessage($"Model: {(VehicleHash)vehicle.Model} -- {vehicle.Price} $");
            }
        }

        public async Task SetVehiclePrice(Player player, string model, double price)
        {
            if (!player.HasData("ID"))
            {
                player.SendChatMessage("Вы не авторизованы");
                return;
            }

            if (!player.GetData<UserModel>("UserModel").IsAdmin)
            {
                player.SendChatMessage("У вас нет таких прав");
                return;
            }

            uint veh = NAPI.Util.GetHashKey(model);

            if (!Enum.IsDefined(typeof(VehicleHash), veh))
            {
                player.SendChatMessage("Такой модели не существует.");
                return;
            }

            VehiclePrice vehiclePrice = new VehiclePrice
            {
                Model = (int)veh,
                Price = price
            };

            await _vehiclePriceRepository.CreateAsync(vehiclePrice);
        }

        public async Task BuyVehicleAsync(Player player, string model, int colorOne, int colorTwo)
        {
            if (!player.HasData("ID"))
            {
                player.SendChatMessage("Вы не авторизованы");
                return;
            }

            uint veh = NAPI.Util.GetHashKey(model);

            if (!Enum.IsDefined(typeof(VehicleHash), veh))
            {
                player.SendChatMessage("Такой модели не существует.");
                return;
            }

            var vehicle = (await _vehiclePriceRepository.GetValuesAsync())
                .FirstOrDefault(x => (uint)x.Model == veh);

            if (vehicle is null)
            {
                player.SendChatMessage($"Данная модель ({(VehicleHash)vehicle.Model}) не продается");
                return;
            }

            var userModel = player.GetData<UserModel>("UserModel");

            if (vehicle.Price > userModel.Money)
            {
                player.SendChatMessage($"У вас не хватает денег. Ваш баланс: {userModel.Money} $");
            }

            userModel.Money -= vehicle.Price;

            await _userModelRepository.UpdateAsync(userModel);


            VehicleModel vehModel = new VehicleModelMapper().ToMap(player, veh, colorOne, colorTwo, vehicle.Price);

            await _vehicleRepository.CreateAsync(vehModel);

            player.SendNotification($"Автомобиль {model} успешно куплен!");
        }

        public async Task SpawnAsync(Player player, string model)
        {
            await SpawnAsync(player, NAPI.Util.GetHashKey(model));
        }
        public async Task SpawnAsync(Player player, uint modelHash)
        {
            if (!player.HasData("ID"))
            {
                player.SendChatMessage("Вы не авторизованы");
            }

            int id = player.GetData<int>("ID");

            var vehicleModel = (await _vehicleRepository
                .GetVehiclesByOwnerIdAsync(id))
                .FirstOrDefault(x => x.Model == (int)modelHash);

            Vector3 vehiclePos = new Vector3(
                vehicleModel.OldPosition[0],
                vehicleModel.OldPosition[1],
                vehicleModel.OldPosition[2]);

            NAPI.Task.Run(() =>
            {
                var veh = NAPI.Vehicle.CreateVehicle(
                     modelHash,
                     vehiclePos,
                     vehicleModel.Rotation,
                     vehicleModel.ColorOne,
                     vehicleModel.ColorTwo,
                     vehicleModel.Number);

                veh.SetData("OwnerId", id);
                veh.SetData("VehicleModel", vehicleModel);
                veh.SetData("ID", vehicleModel.Id);
            }, 1000);

            SpawnedVehicles.Add(vehicleModel.Id);
        }

        public async Task DespawnAsync(Player player, int vehicleId)
        {
            if (!player.HasData("ID"))
            {
                player.SendChatMessage("Вы не авторизованы!");
                return;
            }

            int id = player.GetData<int>("ID");

            Vehicle targetVehicle = NAPI.Pools
                .GetAllVehicles()
                .FirstOrDefault(veh => veh.GetData<int>("ID") == vehicleId);

            if (targetVehicle is null || !SpawnedVehicles.Contains(vehicleId))
            {
                player.SendChatMessage("Данной машины нет на карте");
                return;
            }

            VehicleModel vehModel = targetVehicle.GetData<VehicleModel>("VehicleModel");

            if (vehModel.OwnerID != id)
            {
                player.SendChatMessage("Машина не ваша");

                return;
            }

            targetVehicle.Delete();

            SpawnedVehicles.Remove(vehicleId);

            player.SendNotification("Автомобиль удален");
        }

        public async Task ParkAsync(Player player)
        {
            if (!player.HasData("ID"))
            {
                player.SendChatMessage("Вы не авторизованы!");
                return;
            }

            if (!player.IsInVehicle)
            {
                player.SendChatMessage("Нужно сесть в машину!");
                return;
            }

            var id = player.GetData<int>("ID");

            var vehModel = player.Vehicle.GetData<VehicleModel>("VehicleModel");

            if (vehModel is null)
            {
                player.SendChatMessage("Вы не приобретали данную машину");
                return;
            }

            if (vehModel.OwnerID != id)
            {
                player.SendChatMessage("Машина не ваша!");
                return;
            }

            vehModel.OldPosition = new float[]
            {
                player.Position.X,
                player.Position.Y,
                player.Position.Z
            };

            vehModel.Rotation = player.Vehicle.Rotation.Z;

            await _vehicleRepository.UpdateAsync(vehModel);

            player.SendNotification("Автомобиль успешно припаркован!");
        }

        public async Task SellToPlayer(Player player, string buyerName, int vehicleId, double price)
        {
            if (!player.HasData("ID"))
            {
                player.SendChatMessage("Вы не авторизованы!");
                return;
            }

            var buyer = NAPI.Player.GetPlayerFromName(buyerName);

            if (buyer is null)
            {
                player.SendChatMessage($"Игрок с ником {buyerName} не найден");
                return;
            }

            if (buyer.Position.DistanceTo2D(player.Position) > 5)
            {
                player.SendChatMessage($"Игрок {buyerName} далеко от вас");
                return;
            }

            var veh = (await _vehicleRepository.GetByIdAsync(vehicleId));

            buyer.SendChatMessage(
                $"Игрок {player.Name} хочет продать вам автомобиль {(VehicleHash)veh.Model} за {price} $");

            Seller seller = new Seller
            {
                Name = player.Name,
                VehicleModel = veh,
                Price = price,
            };

            buyer.SetData("Seller", seller);
        }

        public async Task SellToCountry(Player player, int vehicleId)
        {
            if (!player.HasData("ID"))
            {
                player.SendChatMessage("Вы не авторизованы!");
                return;
            }


            var veh = (await _vehicleRepository.GetByIdAsync(vehicleId));

            if (veh.OwnerID != player.GetData<int>("ID"))
            {
                player.SendChatMessage("Машина не ваша");
                return;
            }

            Vehicle targetVehicle = NAPI.Pools
                .GetAllVehicles()
                .FirstOrDefault(veh => veh.GetData<int>("ID") == vehicleId);

            if (targetVehicle != null)
            {
                VehicleModel vehModel = targetVehicle.GetData<VehicleModel>("VehicleModel");

                targetVehicle.Delete();
            }

            var userModel = player.GetData<UserModel>("UserModel");
            userModel.Money += veh.Price;

            await _userModelRepository.UpdateAsync(userModel);

            await _vehicleRepository.DeleteAsync(vehicleId);
        }
    }
}
