using DbModels;
using GTANetworkAPI;
using Provider;
using Repository;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Services
{
    public class RegistrationService
    {
        private readonly LoginModelRepository _loginModelRepository;
        private readonly UserModelRepository _userModelRepository;
        public RegistrationService()
        {
            var db = new RageDbContext();

            _loginModelRepository = new LoginModelRepository(db);
            _userModelRepository = new UserModelRepository(db);
        }

        public async Task RegisterAsync(Player player, string username, string password)
        {
            if (!(await IsRegisterValidate(player, username, password)))
            {
                return;
            }

            LoginModel loginModel = new LoginModel()
            {
                Name = username,
                Password = new MD5Crypt().GetHashToPassword(password)
            };

            var id = await _loginModelRepository.CreateAsync(loginModel);

            await _userModelRepository.CreateAsync(new UserModel()
            {
                LoginModelId = id,
                Money = 10000,
            });

            player.SendChatMessage("Аккаунт зарегистрирован!");
        } 

        public async Task<bool> IsRegisterValidate(Player player, string username, string password)
        {
            if (player.HasData("ID"))
            {
                player.SendChatMessage("Вы уже вошли");
                return false;
            }

            if (username.Length <= 5 || password.Length <= 5)
            {
                player.SendChatMessage("Один из параметров короткий. Min: 6 символов");
                return false;
            }

            var user = await _loginModelRepository.GetByUserNameAsync(username);

            if (user != null)
            {
                player.SendChatMessage("Такой логин занят");
                return false;
            }

            return true;
        }
    }
}
