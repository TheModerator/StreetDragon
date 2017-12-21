using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetDragon.Modules
{
    public class Cele : ModuleBase<SocketCommandContext>
    {
        [Command("cele"), Alias("Celesteon")]
        [Summary("Displays the signature expressions of our local pro-gamer!")]
        public async Task Celesteon()
        {
            await ReplyAsync("ok -.-");
            await ReplyAsync("-is playing Titanfall-");
        }

    }
}
