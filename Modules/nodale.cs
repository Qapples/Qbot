using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
namespace SCPFBot.Modules
{
    public class Nodale : ModuleBase<SocketCommandContext>
    {
        [Command("nodale")]
        public async Task nodale()
        {
            IGuildUser user = (IGuildUser)Context.User;
            Console.WriteLine("dale");
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == "Looking For Game");
            await user.RemoveRoleAsync(role);
        }
    }
}
