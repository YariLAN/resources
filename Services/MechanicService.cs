using DbModels;
using GTANetworkAPI;
using Mappers;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public struct VehServicesType
    {
        public const int TypeId = 402;

        public const string Name = "Repair Vehicles";
    }

    public class MechanicService
    {
        private readonly UnitOfRepositoryBlips _unitOfRepositoryBlips;

        public static List<ColShape> VehicleServicesPoints = new List<ColShape>();

        public MechanicService() 
        {
            _unitOfRepositoryBlips = new UnitOfRepositoryBlips(new Provider.RageDbContext());
        }

        private bool Validate(Player player)
        {
            if (!player.HasData("ID"))
            {
                player.SendChatMessage("Вы не авторизованы!");
                return false;
            }

            var userModel = player.GetData<UserModel>("UserModel");

            if (!userModel.IsAdmin)
            {
                player.SendChatMessage("Вы не обладаете правами админа!");
                return false;
            }

            return true;
        }

        private void CreateColShape(
            Vector3 playerPos, MarkerType markerType, Vector3 pos, float scale, Color color, string blipName, int sprite, byte blipColor)
        {
            NAPI.Task.Run(() =>
            {
                NAPI.Marker.CreateMarker(markerType, pos, new Vector3(), new Vector3(), scale, color);

                NAPI.TextLabel.CreateTextLabel($"Come here to fix your vehicle", playerPos, 1.0f, 1.0f, 0, color, true);

                var shape = NAPI.ColShape.CreateCylinderColShape(playerPos, 1.0f, 1.0f);

                NAPI.Blip.CreateBlip(sprite, playerPos, 1f, blipColor, blipName, 255, 0f, true);

                VehicleServicesPoints.Add(shape);
            }, 2000);
        }

        public async Task Create(Player player)
        {
            if (!Validate(player))
            {
                return;
            }

            var position = player.Position.Add(new Vector3(0, 0, -1));

            var (type, blip) = BlipsDbMapper.ToMap(VehServicesType.Name, VehServicesType.TypeId, position);

            await _unitOfRepositoryBlips.CreateAsync(type, blip);

            CreateColShape(
                player.Position, (MarkerType)1, position, 5.0f, new Color(0, 255, 0, 255), VehServicesType.Name, VehServicesType.TypeId, 12);
        }

        public async Task GetVehicleServices(Player player)
        {
            var positions = await _unitOfRepositoryBlips.GetBlipsAsync(VehServicesType.TypeId);     
            
            foreach (var position in positions)
            {
                Vector3 vec = new Vector3(position[0], position[1], position[2]);
                Vector3 playerPos = vec.Add(new Vector3(0, 0, 1.0f));

                CreateColShape(
                    playerPos, (MarkerType)1, vec, 5.0f, new Color(0, 255, 0, 255), VehServicesType.Name, VehServicesType.TypeId, 12);
            }
        }

        public void OnEnterColShape(ColShape shape, Player player)
        {
            if (player.IsInVehicle)
            {
                foreach (ColShape col in VehicleServicesPoints)
                {
                    if (shape.Equals(col))
                    {
                        player.Vehicle.Repair();

                        player.SendNotification($"You have fixed your {player.Vehicle.DisplayName}");
                    }
                }
            }
        }
    }
}
