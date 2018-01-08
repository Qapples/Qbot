using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
namespace SCPFBot
{
    public class Getrole : ModuleBase<SocketCommandContext>
    {
        [Command("getrole")]
        public async Task getrole(string region)
        {
            /*
            IGuildUser user = (IGuildUser)Context.User;
            Console.WriteLine("getrole");
            var role = Context.Guild.Roles.FirstOrDefault(x => x.Name.ToString() == region);
            await user.AddRoleAsync(role);
            */
            
        }
    }
}
