using GTANetworkAPI;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewResource
{
    public class Registration : Script
    {
        [Command("register", SensitiveInfo = true)]
        public async Task CMD_Register(Player player, string username, string password)
        {
            var register = new RegistrationService();

            await register.RegisterAsync(player, username, password);
        }

        [Command("login", SensitiveInfo = true)]
        public async Task CMD_Login(Player player, string username, string password)
        {
            var loginService = new LoginService();
            
            var isLogin = await loginService.LoginAsync(player, username, password);

            if (isLogin)
            {
                Main.LoggedInIDs.Add(player.GetData<int>("ID"));

                await new PlayerService().SpawnedVehiclesEvent(player);
            }
        }

        [RemoteEvent("LoginFromClient")]
        public async Task LoginfromClient(Player player, string username, string password)
        {
            var loginService = new LoginService();

            var isLogin = await loginService.LoginAsync(player, username, password);

            if (isLogin)
            {
                Main.LoggedInIDs.Add(player.GetData<int>("ID"));

                await new PlayerService().SpawnedVehiclesEvent(player);
            }
        }
    }
}

            