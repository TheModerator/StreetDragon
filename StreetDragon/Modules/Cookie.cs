using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetDragon.Modules
{
    public class Cookie : ModuleBase<SocketCommandContext>
    {
        [Command("cookie"), Alias("Cookie")]
        [Summary("Give a cookie to someone (defaults to yourself)!")]
        public async Task Cookies(SocketGuildUser user = null)
        {
            if (user == null)
            {
                await ReplyAsync($"*Gives a cookie to {Context.User.Mention}~!*");
            } else
            await ReplyAsync($"*Gives a cookie to {user.Mention}~!*");

        }
    }
}
