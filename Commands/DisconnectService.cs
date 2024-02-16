using DbModels;
using GTANetworkAPI;
using Provider;
using Repository;
using System.Threading.Tasks;

namespace Services
{
    public class DisconnectService
    {
        private readonly UserModelRepository _repository;

        public DisconnectService()
        {
            _repository = new UserModelRepository(new RageDbContext());
        }

        public async Task ExecuteAsync(Player player)
        {
            if (!player.HasData("ID"))
            {
                return;
            }

            UserModel userModel = player.GetData<UserModel>("UserModel");

            userModel.OldPosition = new double[]
            {
                player.Position.X,
                player.Position.Y,
                player.Position.Z
            };

            await _repository.UpdateAsync(userModel);

            NAPI.Chat.SendChatMessageToAll($"Игрок с ником {userModel.Name} вышел");
        }
    }
}
