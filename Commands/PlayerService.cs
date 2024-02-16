using DbModels;
using GTANetworkAPI;
using Provider;
using Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class PlayerService
    {
        private readonly UserModelRepository _userRepository;
        private readonly VehicleModelRepository _vehicleModelRepository;
        private readonly VehiclesService _vehiclesService;
        public PlayerService()
        {
            _userRepository = new UserModelRepository(new RageDbContext());
            _vehicleModelRepository = new VehicleModelRepository();

            _vehiclesService = new VehiclesService();
        }
        public async Task SetName(Player player, string name)
        {
            if (!player.HasData("ID"))
            {
                player.SendChatMessage("Вы не авторизованы!");
                return;
            }

            var pl = (await _userRepository.GetValuesAsync()).FirstOrDefault(x => x.Name == name);

            if (pl != null)
            {
                player.SendChatMessage($"Игрок с ником {name} уже существует");
                return;
            }

            var userModel = player.GetData<UserModel>("UserModel");

            userModel.Name = name;
            player.Name = name;

            await _userRepository.UpdateAsync(userModel);

            player.SendChatMessage("Ник успешно обновлен");
        }

        public async Task SetAdmin(Player player)
        {
            if (!player.HasData("ID"))
            {
                player.SendChatMessage("Вы не авторизованы!");
                return;
            }

            var userModel = player.GetData<UserModel>("UserModel");

            userModel.IsAdmin = true;

            await _userRepository.UpdateAsync(userModel);

            player.SendChatMessage("Вам выданы права администратора)");
        }

        public async Task GetVehicle(Player admin, string model, int colorOne = 0, int colorTwo = 0)
        {
            if (!admin.HasData("ID"))
            {
                admin.SendChatMessage("Вы не авторизованы!");
                return;
            }

            var id = admin.GetData<int>("ID");

            var userModel = admin.GetData<UserModel>("UserModel");

            if (!userModel.IsAdmin)
            {
                admin.SendChatMessage("У вас не прав администратора");
                return;
            }

            if (admin.IsInVehicle)
            {
                admin.SendChatMessage("Вы уже в машине!");
                return;
            }

            var modelHash = (VehicleHash)NAPI.Util.GetHashKey(model);

            var pos = NAPI.Entity.GetEntityPosition(admin);

            var vehicle = NAPI.Vehicle.CreateVehicle(modelHash, pos, 0, colorOne, colorTwo, "ADMIN");

            admin.SetIntoVehicle(vehicle, (int)VehicleSeat.Driver);
        }

        public void AdminExitVehicleEvent(Player admin, Vehicle vehicle)
        {
            var userModel = admin.GetData<UserModel>("UserModel");

            if (!userModel.IsAdmin)
            {
                return;
            }

            vehicle.Delete();
        }

        public void EnterVehicleEvent(Player player, Vehicle vehicle)
        {
            var id = player.GetData<int>("ID");

            if (vehicle.GetData<int>("OwnerId") == id)
            {
                vehicle.Locked = false;
            }
        }

        public async Task SpawnedVehiclesEvent(Player player)
        {
            if (!player.HasData("ID"))
            {
                return;
            }

            var id = player.GetData<int>("ID");

            var vehicles = await _vehicleModelRepository.GetVehiclesByOwnerIdAsync(id);

            foreach(var veh in vehicles)
            {
                await _vehiclesService.SpawnAsync(player, (uint)veh.Model);
            }
        }

        public async Task AcceptSell(Player buyer)
        {
            if (!buyer.HasData("ID"))
            {
                buyer.SendChatMessage("Вы не авторизованы!");
                return;
            }

            if (!buyer.HasData("Seller"))
            {
                buyer.SendChatMessage("Сообщений о продаже не поступало!");
                return;
            }

            var seller = buyer.GetData<Seller>("Seller");

            var veh = seller.VehicleModel;

            var player = NAPI.Player.GetPlayerFromName(seller.Name);

            if (player is null)
            {
                buyer.SendChatMessage($"Игрок {seller.Name} не найден");
                return;
            }

            if (buyer.Position.DistanceTo2D(player.Position) > 5)
            {
                buyer.SendChatMessage($"Игрок {player.Name} далеко от вас");
                return;
            }

            var userModel = buyer.GetData<UserModel>("UserModel");

            if (userModel.Money < seller.Price)
            {
                buyer.SendChatMessage("Вам не хватает средств для совершения покупки");
                return;
            }

            userModel.Money -= seller.Price;

            await _userRepository.UpdateAsync(userModel);

            veh.OwnerID = userModel.Id;
            veh.OldPosition = new float[3];

            await _vehicleModelRepository.UpdateAsync(veh);

            if (VehiclesService.SpawnedVehicles.Contains(veh.Id))
            {
                Vehicle targetVehicle = NAPI.Pools
                .GetAllVehicles()
                .FirstOrDefault(x => x.GetData<int>("ID") == veh.Id);

                targetVehicle.SetData("OwnerId", userModel.Id);

                VehiclesService.SpawnedVehicles.Remove(veh.Id);
            }
        }
    }
}
