using DbModels;
using GTANetworkAPI;
using Repository;
using Provider;
using System.Threading.Tasks;
using System;

namespace Services
{
    public class LoginService
    {
        private readonly LoginModelRepository _repository;
        private readonly UserModelRepository _userModelRepository;

        public LoginService()
        {
            var db = new RageDbContext();

            _repository = new LoginModelRepository(db);
            _userModelRepository = new UserModelRepository(db);
        }

        public async Task<bool> LoginAsync(Player player, string username, string password)
        {
            if (!(await IsLoginValidate(player, username, password)))
            {
                return false;
            }

            var loginId = (await _repository.GetByUserNameAsync(username)).Id;

            var userModel = await _userModelRepository.GetUserModelByLoginId(loginId);

            player.Position = new Vector3(
                userModel.OldPosition[0],
                userModel.OldPosition[1],
                userModel.OldPosition[2]);

            player.SetData("ID", loginId);

            player.SetData("UserModel", userModel);

            player.SendNotification("Отлично, ты успешно вошел!");

            return true;
        }

        public async Task<bool> IsLoginValidate(Player player, string username, string password)
        {
            if (player.HasData("ID"))
            {
                player.SendChatMessage("Ты уже вошел!");
                return false;
            }

            if (username.Length <= 5 || password.Length <= 5)
            {
                player.SendChatMessage("В одном из параметров меньше 6 символов.");
                return false;
            }

            var user = await _repository.GetByUserNameAsync(username);

            if (user is null)
            {
                player.SendChatMessage("Такого игрока не существует. Зарегистрируйтесь!");
                return false;
            }

            var pass = new MD5Crypt().GetHashToPassword(password);

            if (pass != user.Password)
            {
                player.SendChatMessage("Логин или пароль указаны неверно");
                return false;
            }

            return true;
        }
    }
}
