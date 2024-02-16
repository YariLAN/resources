using GTANetworkAPI;
using Services;
using System.Threading.Tasks;

namespace NewResource.Commands
{
    public class MoneyCommands : Script
    {
        [Command("money")]
        public void GetMoney(Player player)
        {
            new MoneyService().GetMoney(player);
        }

        [Command("addmoney")]
        public async Task AddMoney(Player player, double amount)
        {
            await new MoneyService().AddMoney(player, amount);
        }
    }
}
