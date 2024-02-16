using DbModels;
using GTANetworkAPI;
using Repository;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Services
{
    public class MoneyService
    {
        private readonly UserModelRepository _userModelRepository;

        public MoneyService()
        {
            _userModelRepository = new UserModelRepository(new Provider.RageDbContext());
        }

        public void GetMoney(Player player)
        {
            if (!Validate(player))
            {
                return;
            }

            var userModel = player.GetData<UserModel>("UserModel");

            player.SendChatMessage($"Ваш баланс: {userModel.Money} $");
        }

        private bool Validate(Player player)
        {
            if (!player.HasData("ID"))
            {
                player.SendChatMessage("Вы не авторизованы!");
                return false;
            }

            return true;
        }

        public async Task AddMoney(Player player, double money)
        {
            if (!Validate(player))
            {
                return;
            }

            var userModel = player.GetData<UserModel>("UserModel");

            userModel.Money += money;

            await _userModelRepository.UpdateAsync(userModel);
        }

        public bool HasMoney(Player player)
        {
            if (!Validate(player))
            {
                return false;
            }

            return player.GetData<UserModel>("UserModel").Money > 0.0; 
        }
    }
}
