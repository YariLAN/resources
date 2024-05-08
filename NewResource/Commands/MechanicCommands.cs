using GTANetworkAPI;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewResource.Commands
{
    public class MechanicCommands : Script
    {
        [Command("createfixpoint")]
        public async Task CreateVehicleService(Player player)
        {
            await new MechanicService().Create(player); 
        }

        [Command("createcp")]
        public void CreateCP(Player player)
        {
            NAPI.Marker.CreateMarker(
                MarkerType.VerticalCylinder, 
                player.Position - (new Vector3(0, 0, 1.0f)),
                new Vector3(),
                new Vector3(),
                1.0f,
                new Color(255, 0, 0),
                false,
                player.Dimension);

            NAPI.TextLabel.CreateTextLabel(
                "Tuning Vehicle", player.Position, 20.0f, 1.0f, 0, new Color(255, 255, 255), true);

            NAPI.ColShape.CreateCylinderColShape(player.Position, 1.0f, 1.0f);
        }

        [ServerEvent(Event.PlayerEnterColshape)]
        public void OnEnterColShape(ColShape shape, Player player)
        {
            new MechanicService().OnEnterColShape(shape, player);
        }
    }
}
